Imports System.Data.Common
Imports System.Text
Imports System.Text.RegularExpressions
Imports MRFramework
Imports MRFramework.MRPersisting.Core
Imports MRFramework.MRPersisting.Factory

Public Class DBCreator
    Implements IDBChained

    Public Property DBSqlGenerator As IDBSqlGenerator
    Public Property ActiveModuleKeys As New List(Of String)
    Public ReadOnly Property DBModules As New List(Of IDBModule)
    Public ReadOnly Property SourceDBRevisions As New Dictionary(Of String, IDBRevision)

    Public ReadOnly Property SourceDBSqlRevisions As New HashSet(Of DBSqlRevision)(New DBSqlRevision.DBSqlRevisionEqualityComparer)
    Public ReadOnly Property ExecutedDBSqlRevisions As New HashSet(Of DBSqlRevision)(New DBSqlRevision.DBSqlRevisionEqualityComparer)
    Public Property RevisionBatchSize As Integer = 1
    Public Property Parent As IDBChained Implements IDBChained.Parent

    Public Function AddModule(dBModule As IDBModule) As IDBModule
        DBModules.Add(dBModule)
        dBModule.Parent = Me

        Return dBModule
    End Function

    Public Function LoadModuleKeysFromDB() As List(Of IDBModule)
        Dim ret As New List(Of IDBModule)

        Using cnn As DbConnection = MRC.GetConnection
            Try
                If cnn.State <> ConnectionState.Open Then
                    cnn.Open()
                End If

                Using per As New DBModulePersister
                    per.Where = "Active = 1"
                    per.CNN = cnn
                    Dim res As Dictionary(Of Object, IMRDLO) = per.GetData()
                    For Each key As Object In res.Keys
                        ActiveModuleKeys.Add(CStr(key))
                    Next
                End Using
            Catch ex As Exception
                If Debugger.IsAttached Then
                    Debugger.Break()
                End If
                Throw
            End Try

        End Using

        Return ret
    End Function

    Public Sub LoadExecutedDBSqlRevisionsFromDB(cnn As Common.DbConnection, Optional trn As Common.DbTransaction = Nothing)
        ExecutedDBSqlRevisions.Clear()

        Using per As New DBSqlRevision.DBSqlRevisionPersister With {.CNN = cnn, .PagingEnabled = False}
            With per.OrderItems
                .Add(New MRCore.MROrderItem("Created", MRCore.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("Granulation", MRCore.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("ObjectType", MRCore.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("RevisionType", MRCore.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("ModuleKey", MRCore.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("SchemaName", MRCore.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("SchemaObjectName", MRCore.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("ObjectName", MRCore.Enums.eOrderDirection.Ascending))
            End With

            Dim dicExecutedRevisions As Dictionary(Of Object, IMRDLO) = per.GetData(trn)
            For Each kv As KeyValuePair(Of Object, IMRDLO) In dicExecutedRevisions
                Dim sqlRevision As New DBSqlRevision(kv.Value, Me)
                ExecutedDBSqlRevisions.Add(sqlRevision)
            Next
        End Using
    End Sub


    Private Shared Function SplitSqlStatements(sqlScript As String) As IEnumerable(Of String)
        ' Split by "GO" statements
        Dim statements = Regex.Split(sqlScript, "^\s*GO\s* ($ | \-\- .*$)", RegexOptions.Multiline Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.IgnoreCase)

        ' Remove empties, trim, and return
        Return statements.Where(Function(x) Not String.IsNullOrWhiteSpace(x)).[Select](Function(x) x.Trim(" "c, ControlChars.Cr, ControlChars.Lf))
    End Function

    Public Event BatchExecuting(sender As Object, ce As BatchExecutingEventArgs)
    Public Sub OnBatchExecuting(sender As Object, ce As BatchExecutingEventArgs)
        RaiseEvent BatchExecuting(sender, ce)
    End Sub

    Public Event BatchExecuted(sender As Object, e As BatchExecutedEventArgs)
    Public Sub OnBatchExecuted(sender As Object, e As BatchExecutedEventArgs)
        RaiseEvent BatchExecuted(sender, e)
    End Sub

    Private Sub ExecuteRevisionBatch(script As String, revisions As List(Of DBSqlRevision), executedRevisionsCount As Integer, totalRevisionsCount As Integer, cnn As DbConnection, trn As DbTransaction)
        Dim batches As List(Of String) = SplitSqlStatements(script).ToList
        For Each batch As String In batches
            Dim ts1 As TimeSpan
            Dim ts2 As TimeSpan
            Using cmd As IDbCommand = MRC.GetCommand(cnn)
                Dim errorMessage As String = ""
                Try
                    cmd.CommandText = batch
                    cmd.Transaction = trn

                    OnBatchExecuting(Me, New BatchExecutingEventArgs With {.Cancel = False, .Sql = batch})

                    ts1 = New TimeSpan(Now.Ticks)
                    cmd.ExecuteNonQuery()

                    Dim dlos As New List(Of IMRDLO)
                    revisions.ForEach(
                            Sub(rev)
                                Dim dlo As IMRDLO = rev.GetDlo
                                dlo.ColumnValues.Add("ID", Guid.NewGuid)
                                dlo.ColumnValues.Add("Executed", Now())
                                dlos.Add(dlo)
                            End Sub)

                    Using per As New DBSqlRevision.DBSqlRevisionPersister With {.CNN = cnn}
                        per.InsertBulk(dlos, trn)
                    End Using
                    ts2 = New TimeSpan(Now.Ticks)
                Catch ex As SqlClient.SqlException
                    errorMessage = ex.Message
                    Throw
                Catch ex As Exception
                    errorMessage = ex.Message
                    Throw
                Finally
                    OnBatchExecuted(Me, New BatchExecutedEventArgs With {
                                            .Sql = batch,
                                            .Duration = ts2 - ts1,
                                            .ResultType = CType(IIf(errorMessage = "", eBatchExecutionResultType.Success, eBatchExecutionResultType.Failed), eBatchExecutionResultType),
                                            .ExecutedRevisionsCount = executedRevisionsCount,
                                            .TotalRevisionsCount = totalRevisionsCount,
                                            .ErrorMessage = errorMessage
                                        })
                End Try
            End Using
        Next
    End Sub

    Private Sub ExecuteDBSqlRevisionBatches(notExecutedRevisions As List(Of DBSqlRevision), cnn As DbConnection, trn As DbTransaction)
        notExecutedRevisions.Sort(AddressOf DBSqlRevision.CompareRevisionsForDbCreations)

        Dim newExecutedRevisions As New List(Of DBSqlRevision)
        Dim sqlScriptBuilder As New StringBuilder()
        Dim sqlBatchScriptBuilder As New StringBuilder()

        For i As Integer = 0 To notExecutedRevisions.Count - 1
            Dim rev As DBSqlRevision = notExecutedRevisions(i)
            Dim sql As String = rev.Sql
            sqlScriptBuilder.Append(sql)
            sqlBatchScriptBuilder.Append(sql)

            newExecutedRevisions.Add(rev)
            If i = notExecutedRevisions.Count - 1 OrElse (i + 1) Mod RevisionBatchSize = 0 Then
                ExecuteRevisionBatch(sqlBatchScriptBuilder.ToString, newExecutedRevisions, i + 1, notExecutedRevisions.Count, cnn, trn)
                sqlBatchScriptBuilder.Clear()
                newExecutedRevisions.Clear()
            End If
        Next
    End Sub

    Public Sub ExecuteDBSqlRevisions(cnn As DbConnection, trn As DbTransaction)
        Try
            Dim notExecutedRevisions As List(Of DBSqlRevision) = SourceDBSqlRevisions.Where(Function(rev) rev.RevisionType <> eDBRevisionType.AlwaysExecute).Except(ExecutedDBSqlRevisions, New DBSqlRevision.DBSqlRevisionEqualityComparer).ToList
            Dim alwaysExecutingTasks As List(Of DBSqlRevision) = SourceDBSqlRevisions.Where(Function(rev) rev.RevisionType = eDBRevisionType.AlwaysExecute).ToList

            ExecuteDBSqlRevisionBatches(notExecutedRevisions, cnn, trn)
            ExecuteDBSqlRevisionBatches(alwaysExecutingTasks, cnn, trn)
        Catch ex As Exception
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
        End Try
    End Sub

#Region "System objects"

    Private Sub CreateRevisionTable()
        Using cnn As IDbConnection = MRC.GetConnection
            Using cmd As IDbCommand = MRC.GetCommand()
                Try
                    cmd.Connection = cnn
                    If cnn.State <> ConnectionState.Open Then
                        cnn.Open()
                    End If

                    cmd.CommandText = "
IF OBJECT_ID('DBCreator.Revision') IS NULL
BEGIN
	CREATE TABLE [DBCreator].[Revision]
	(
		[ID] [uniqueidentifier] NOT NULL PRIMARY KEY NONCLUSTERED,
		[Created] [date] NOT NULL,
		[Granulation] [int] NOT NULL,
        [ObjectType] [varchar](50) NOT NULL,        
        [RevisionType] [varchar](50) NOT NULL,
        [ModuleKey] [varchar](50),
        [SchemaName] [varchar](50),
        [SchemaObjectName] [varchar](150),
        [ObjectName] [varchar](150) NOT NULL,
		[Executed] [datetime] NOT NULL CONSTRAINT DF_DBCreator_Revision_Executed DEFAULT GETDATE(),
        [ObjectFullName] [varchar](100) NOT NULL,
        [Description] [nvarchar](MAX) NULL
	)
	IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name='IX_DBCreatorRevision_Clustered' AND object_id = OBJECT_ID('DBCreator.Revision'))
	BEGIN
		DROP INDEX IX_DBCreatorRevision_Clustered ON DBCreator.Revision 
	END
	CREATE CLUSTERED INDEX IX_DBCreatorRevision_Clustered ON DBCreator.Revision (Created, Granulation, ObjectType, RevisionType, ModuleKey, SchemaName, SchemaObjectName, ObjectName)
END
"
                    cmd.ExecuteNonQuery()

                Catch ex As Exception
                    If Debugger.IsAttached Then
                        Debugger.Break()
                    End If
                    Throw
                End Try
            End Using
        End Using
    End Sub

    Private Sub CreateSchema()
        Using cnn As IDbConnection = MRC.GetConnection
            Using cmd As IDbCommand = MRC.GetCommand()
                Try
                    cmd.Connection = cnn
                    If cnn.State <> ConnectionState.Open Then
                        cnn.Open()
                    End If

                    cmd.CommandText = "SELECT TOP 1 1 FROM sys.schemas WHERE name = 'DBCreator'"
                    If cmd.ExecuteScalar() Is Nothing Then
                        cmd.CommandText = "CREATE SCHEMA DBCreator"
                        cmd.ExecuteNonQuery()
                    End If
                Catch ex As Exception
                    If Debugger.IsAttached Then
                        Debugger.Break()
                    End If
                    Throw
                End Try
            End Using
        End Using
    End Sub

    Private Sub CreateModuleTable()
        Using cnn As IDbConnection = MRC.GetConnection
            Using cmd As IDbCommand = MRC.GetCommand()
                Try
                    cmd.Connection = cnn
                    If cnn.State <> ConnectionState.Open Then
                        cnn.Open()
                    End If

                    cmd.CommandText = "
IF OBJECT_ID('DBCreator.Module') IS NULL
BEGIN
	CREATE  TABLE [DBCreator].[Module]
	(
        [ModuleKey] [varchar](50) NOT NULL PRIMARY KEY,
        [Name] [nvarchar](50) NOT NULL,
        [Created] [datetime] NOT NULL,
        [Active] bit NOT NULL,
        [Description] [nvarchar](MAX) NULL
	)
END
"
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    If Debugger.IsAttached Then
                        Debugger.Break()
                    End If
                    Throw
                End Try
            End Using
        End Using
    End Sub

    Public Sub CreateSystemObjects()
        CreateSchema()
        CreateRevisionTable()
        CreateModuleTable()
    End Sub

#End Region

End Class
