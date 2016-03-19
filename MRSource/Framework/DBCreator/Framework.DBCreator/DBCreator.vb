Imports System.Text
Imports System.Text.RegularExpressions
Imports MRFramework
Imports MRFramework.MRPersisting.Core
Imports MRFramework.MRPersisting.Factory

Public Class DBCreator
    Implements IDBChained

    Public ReadOnly Property DBModules As New List(Of IDBModule)
    Public ReadOnly Property SourceDBSqlRevisions As New List(Of DBSqlRevision)
    Public ReadOnly Property ExecutedDBSqlRevisions As New List(Of DBSqlRevision)
    Public ReadOnly Property DBSchemas As New Dictionary(Of String, IDBObject)
    Public ReadOnly Property DBTables As New Dictionary(Of String, IDBObject)
    Public ReadOnly Property DBFields As New Dictionary(Of String, IDBObject)
    Public Property RevisionBatchSize As Integer = 10
    Public Property Parent As IDBChained Implements IDBChained.Parent

    Public Function AddModule(dBModule As IDBModule) As IDBModule
        DBModules.Add(dBModule)
        dBModule.Parent = Me

        Return dBModule
    End Function

    Public Function GetModules() As List(Of IDBModule)
        Dim ret As New List(Of IDBModule)

        ret.AddRange(DBModules)

        Return ret
    End Function

    Public Sub LoadExecutedDBSqlRevisionsFromDB(cnn As Common.DbConnection, Optional trn As Common.DbTransaction = Nothing)
        ExecutedDBSqlRevisions.Clear()

        Using per As New DBSqlRevision.DBSqlRevisionPersister With {.CNN = cnn, .PagingEnabled = False}
            With per.OrderItems
                .Add(New MRCore.MROrderItem("Created", MRCore.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("Granulation", MRCore.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("DBObjectType", MRCore.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("DBRevisionType", MRCore.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("DBObjectFullName", MRCore.Enums.eOrderDirection.Ascending))
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
                Catch ex As Exception
                    If Debugger.IsAttached Then
                        Debugger.Break()
                    End If
                    Throw
                End Try
            End Using
        Next
    End Sub

    Public Function ExecuteDBSqlRevisions(cnn As Common.DbConnection, trn As Common.DbTransaction) As String
        Dim notExecutedRevisions = SourceDBSqlRevisions.Except(ExecutedDBSqlRevisions, New DBSqlRevision.DBSqlRevisionEqualityComparer).ToList()

        Dim newExecutedRevisions As New List(Of DBSqlRevision)
        Dim sqlScriptBuilder As New StringBuilder()
        Dim sqlBatchScriptBuilder As New StringBuilder()

        For i As Integer = 0 To notExecutedRevisions.Count - 1
            Dim rev As DBSqlRevision = notExecutedRevisions(i)
            Dim sql As String
            Select Case rev.DBRevisionType
                Case eDBRevisionType.Create
                    sql = rev.Parent.Parent.GetSqlCreate()
                    sqlScriptBuilder.Append(sql)
                    sqlBatchScriptBuilder.Append(sql)
                Case eDBRevisionType.Modify
                    sql = rev.Parent.Parent.GetSqlModify()
                    sqlScriptBuilder.Append(sql)
                    sqlBatchScriptBuilder.Append(sql)
                Case eDBRevisionType.Delete
                    sql = rev.Parent.Parent.GetSqlDelete()
                    sqlScriptBuilder.Append(sql)
                    sqlBatchScriptBuilder.Append(sql)
                Case Else
                    Throw New NotSupportedException("eDBRevisionType")
            End Select
            rev.Description = sql
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
            Using cmd As IDbCommand = MRC.GetCommand
                Try
                    cmd.CommandText =
<string>
IF OBJECT_ID('DBCreator.Revision') IS NULL
BEGIN
	CREATE TABLE [DBCreator].[Revision]
	(
		[ID] [uniqueidentifier] NOT NULL PRIMARY KEY NONCLUSTERED,
		[Created] [date] NOT NULL,
		[Granulation] [int] NOT NULL,
		[DBObjectFullName] [varchar](250) NOT NULL,
        [DBObjectName] [varchar](50) NOT NULL,
		[DBObjectType] [varchar](50) NOT NULL,
		[DBRevisionType] [varchar](50) NOT NULL,
        [SchemaName] [varchar](50),
        [Description] [nvarchar](max) NULL,
	)
END
IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name='IX_DBCreatorRevision_Sort' AND object_id = OBJECT_ID('DBCreator.Revision'))
BEGIN
	DROP INDEX IX_DBCreatorRevision_Sort ON DBCreator.Revision 
END
CREATE CLUSTERED INDEX IX_DBCreatorRevision_Sort ON DBCreator.Revision (Created ASC, Granulation ASC, DBObjectType ASC, DBRevisionType ASC, DBObjectFullName ASC)

</string>.Value
                    cmd.Connection = cnn
                    If cnn.State <> ConnectionState.Open Then
                        cnn.Open()
                    End If

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
        CreateRevisionTable()
    End Sub

#End Region

End Class
