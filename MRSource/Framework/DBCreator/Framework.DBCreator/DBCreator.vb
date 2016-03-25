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


    Public ReadOnly Property DBTables As Dictionary(Of String, IDBObject)
        Get
            ' TODO - procitati sve tableove chiled objekata pocevsi od dbmodule
            Throw New NotImplementedException
        End Get
    End Property
    Public ReadOnly Property DBFields As Dictionary(Of String, IDBObject)
        Get
            ' TODO - procitati sve fieldove chiled objekata pocevsi od dbmodule
            Throw New NotImplementedException
        End Get
    End Property

    Public Property RevisionBatchSize As Integer = 10
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

            ' TODO - per.GetData generira bezvezan query, treba smisliti nesto drugo, stignem
            ' ovako:
            ' SELECT Case ID, Created, Granulation, DBObjectType, DBRevisionType, DBObjectFullName, DBObjectName, SchemaName FROM Common.Revision
            ' ORDER BY  Created ASC, Granulation ASC, DBObjectType ASC, DBRevisionType ASC, DBObjectFullName ASC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY

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

    Private Sub ExecuteRevisionBatch(script As String, revisions As List(Of DBSqlRevision), cnn As Common.DbConnection, trn As Common.DbTransaction)
        Dim batches As List(Of String) = SplitSqlStatements(script).ToList
        For Each batch As String In batches
            Using cmd As IDbCommand = MRC.GetCommand(cnn)
                Try
                    cmd.CommandText = batch
                    cmd.Transaction = trn
                    cmd.ExecuteNonQuery()
                    Dim dlos As New List(Of IMRDLO)
                    For Each rev As DBSqlRevision In revisions
                        Dim dlo As IMRDLO = rev.GetDlo
                        dlo.ColumnValues.Add("ID", Guid.NewGuid)
                        dlos.Add(dlo)
                    Next
                    Using per As New DBSqlRevision.DBSqlRevisionPersister With {.CNN = cnn}
                        per.InsertBulk(dlos, trn)
                    End Using
                Catch ex As SqlClient.SqlException
                    If Debugger.IsAttached Then
                        Debugger.Break()
                    End If
                    Throw
                Catch ex As Exception
                    If Debugger.IsAttached Then
                        Debugger.Break()
                    End If
                    Throw
                End Try
            End Using
        Next
    End Sub

    Public Function ExecuteDBSqlRevisions(cnn As DbConnection, trn As DbTransaction) As String
        Dim notExecutedRevisions As List(Of DBSqlRevision) = SourceDBSqlRevisions.Except(ExecutedDBSqlRevisions, New DBSqlRevision.DBSqlRevisionEqualityComparer).ToList
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
                ExecuteRevisionBatch(sqlBatchScriptBuilder.ToString, newExecutedRevisions, cnn, trn)
                sqlBatchScriptBuilder.Clear()
                newExecutedRevisions.Clear()
            End If
        Next

        Return sqlScriptBuilder.ToString()
    End Function

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
