Imports System.Data.Common
Imports System.Text
Imports MRFramework
Imports MRFramework.MRPersisting.Core
Imports MRFramework.MRPersisting.Factory

Public Class DBTimeLiner
    Implements IDBChained

#Region "Public properties"
    Public Property DBSqlGenerator As IDBSqlGenerator
    Public ReadOnly Property DBModules As New List(Of IDBModule)
    Public ReadOnly Property SourceDBRevisions As New Dictionary(Of String, IDBRevision)
    Public ReadOnly Property SourceDBSqlRevisions As New HashSet(Of DBSqlRevision)(New DBSqlRevision.DBSqlRevisionEqualityComparer)
    Public ReadOnly Property ExecutedDBSqlRevisions As New HashSet(Of DBSqlRevision)(New DBSqlRevision.DBSqlRevisionEqualityComparer)
    Public ReadOnly Property ExecutedDBRevisions As New Dictionary(Of String, IDBRevision)

    Public ReadOnly Property NewDBSqlRevisions As List(Of DBSqlRevision)
        Get
            Return SourceDBSqlRevisions.Where(Function(rev) rev.RevisionType <> eDBRevisionType.AlwaysExecuteTask).Except(ExecutedDBSqlRevisions, New DBSqlRevision.DBSqlRevisionEqualityComparer).ToList
        End Get
    End Property

    Private _NewDBRevisions As New List(Of IDBRevision)
    Public ReadOnly Property NewDBRevisions As List(Of IDBRevision)
        Get
            _NewDBRevisions.Clear()
            For Each sqlRev In NewDBSqlRevisions
                _NewDBRevisions.Add(sqlRev.Parent)
            Next

            Return _NewDBRevisions
        End Get
    End Property

    Public Property RevisionBatchSize As Integer = 1
    Public Property Parent As IDBChained Implements IDBChained.Parent
    Public Property DBType As eDBType = eDBType.TransactSQL
#End Region

#Region "Constructor"
    Public Sub New(dBType As eDBType, dbSqlFactory As IDBSqlGeneratorFactory)
        Me.DBType = dBType
        DBSqlGenerator = dbSqlFactory.GetDBSqlGenerator(dBType)
    End Sub
#End Region

#Region "Events and event raisers"

    Public Event BatchExecuting(sender As Object, ce As BatchExecutingEventArgs)
    Public Sub OnBatchExecuting(sender As Object, ce As BatchExecutingEventArgs)
        RaiseEvent BatchExecuting(sender, ce)
    End Sub

    Public Event BatchExecuted(sender As Object, e As BatchExecutedEventArgs)
    Public Sub OnBatchExecuted(sender As Object, e As BatchExecutedEventArgs)
        RaiseEvent BatchExecuted(sender, e)
    End Sub

    Public Event ProgressReported(sender As Object, e As ProgressReportedEventArgs)
    Public Sub OnProgressReported(sender As Object, e As ProgressReportedEventArgs)
        RaiseEvent ProgressReported(sender, e)
    End Sub

#End Region

#Region "Public methods"

    Public Sub LoadExecutedDBSqlRevisionsFromDB(cnn As Common.DbConnection, Optional trn As Common.DbTransaction = Nothing)
        ExecutedDBSqlRevisions.Clear()

        Using per As New DBSqlRevision.DBSqlRevisionPersister With {.CNN = cnn, .PagingEnabled = False}
            With per.OrderItems
                .Add(New MRCore.MROrderItem("RevisionKey", MRCore.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("ID", MRCore.Enums.eOrderDirection.Ascending))
            End With

            Dim dicExecutedRevisions As Dictionary(Of Object, IMRDLO) = per.GetData(trn)
            For Each kv As KeyValuePair(Of Object, IMRDLO) In dicExecutedRevisions
                Dim sqlRevision As New DBSqlRevision(kv.Value, Me)
                ExecutedDBSqlRevisions.Add(sqlRevision)
                ExecutedDBRevisions.Add(sqlRevision.Key, sqlRevision.Parent)
            Next
        End Using
    End Sub

    Public Sub ExecuteDBSqlRevisions(cnn As DbConnection, trn As DbTransaction)
        Try
            ExecuteDBSqlRevisionBatches(NewDBSqlRevisions, cnn, trn, False)
        Catch ex As Exception
            ' CONSIDER - do some logging
            Throw
        End Try
    End Sub

    Public Sub ExecuteDBSqlRevisionsAlwaysExecutingTasks(cnn As DbConnection, trn As DbTransaction)
        Try
            Dim alwaysExecutingTasks As List(Of DBSqlRevision) = SourceDBSqlRevisions.Where(Function(rev) rev.RevisionType = eDBRevisionType.AlwaysExecuteTask).ToList

            ExecuteDBSqlRevisionBatches(alwaysExecutingTasks, cnn, trn, True)
        Catch ex As Exception
            ' CONSIDER - do some logging
            Throw
        End Try
    End Sub


    Public Sub CreateSystemObjects()
        CreateSchema()
        CreateRevisionTable()
        CreateAlwaysExecutingTaskTable()
        CreateModuleTable()
        CreateCustomizationTable()
    End Sub

#End Region

#Region "Private methods"

    Private Sub ExecuteScriptBatches(script As String, cnn As DbConnection, trn As DbTransaction, cancelEvents As Boolean)
        Dim batches As List(Of String) = DBSqlGenerator.SplitSqlStatements(script).ToList

        For Each batch As String In batches
            Dim ts1 As TimeSpan
            Dim ts2 As TimeSpan
            Using cmd As IDbCommand = MRC.GetCommand(cnn)
                Dim errorMessage As String = ""
                Dim runtimeException As Exception = Nothing
                Try
                    cmd.CommandText = batch
                    cmd.Transaction = trn

                    If Not cancelEvents Then
                        OnBatchExecuting(Me, New BatchExecutingEventArgs With {.Cancel = False, .Sql = batch})
                        ts1 = New TimeSpan(Now.Ticks)
                    End If

                    cmd.ExecuteNonQuery()

                    If Not cancelEvents Then
                        ts2 = New TimeSpan(Now.Ticks)
                    End If
                Catch ex As SqlClient.SqlException
                    'If Debugger.IsAttached Then
                    '    Debugger.Break()
                    'End If
                    runtimeException = ex
                    errorMessage = ex.Message
                    Throw
                Catch ex As Exception
                    If Debugger.IsAttached Then
                        Debugger.Break()
                    End If
                    runtimeException = ex
                    errorMessage = ex.Message
                    Throw
                Finally
                    If Not cancelEvents Then
                        OnBatchExecuted(Me, New BatchExecutedEventArgs With {
                                            .Sql = batch,
                                            .Duration = ts2 - ts1,
                                            .ResultType = CType(IIf(errorMessage = "", eBatchExecutionResultType.Success, eBatchExecutionResultType.Failed), eBatchExecutionResultType),
                                            .ErrorMessage = errorMessage,
                                            .Exception = runtimeException
                                        })
                    End If
                End Try
            End Using
        Next
    End Sub

    Private Sub ExecuteRevisionBatch(script As String, revisions As List(Of DBSqlRevision), executedRevisionsCount As Integer, totalRevisionsCount As Integer, cnn As DbConnection, trn As DbTransaction, alwaysExecutingTask As Boolean)
        ExecuteScriptBatches(script, cnn, trn, False)

        Dim dlos As New List(Of IMRDLO)
        revisions.ForEach(
                            Sub(rev)
                                Dim dlo As IMRDLO = Nothing
                                dlo = rev.GetDlo()
                                dlo.ColumnValues.Add("ID", Guid.NewGuid)
                                dlo.ColumnValues.Add("Executed", Now())
                                dlos.Add(dlo)
                            End Sub)
        Dim per As IMRPersister = Nothing
        Try
            If alwaysExecutingTask Then
                per = New DBSqlRevision.DBSqlAlwaysExecutingTaskPersister With {.CNN = cnn}
            Else
                per = New DBSqlRevision.DBSqlRevisionPersister With {.CNN = cnn}
            End If

            per.InsertBulk(dlos, trn)
        Catch
            Throw
        Finally
            per.Dispose()
            per = Nothing
        End Try
    End Sub

    Private Sub ExecuteDBSqlRevisionBatches(notExecutedRevisions As List(Of DBSqlRevision), cnn As DbConnection, trn As DbTransaction, alwaysExecutingTask As Boolean)
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
                ExecuteRevisionBatch(sqlBatchScriptBuilder.ToString, newExecutedRevisions, i + 1, notExecutedRevisions.Count, cnn, trn, alwaysExecutingTask)
                sqlBatchScriptBuilder.Clear()
                newExecutedRevisions.Clear()
                Dim msg As String = "New revisions"
                If alwaysExecutingTask Then
                    msg = "Always executing tasks"
                End If
                OnProgressReported(Me, New ProgressReportedEventArgs() With {.CurrentStep = i + 1, .TotalSteps = notExecutedRevisions.Count, .Message = msg})
            End If
        Next
    End Sub

#Region "System objects"

    Private Sub CreateAlwaysExecutingTaskTable()
        Using cnn As IDbConnection = MRC.GetConnection
            Try
                If cnn.State <> ConnectionState.Open Then
                    cnn.Open()
                End If

                ExecuteScriptBatches(DBSqlGenerator.GetSqlCreateSystemAlwaysExecutingTaskTable(), CType(cnn, DbConnection), Nothing, True)
            Catch ex As Exception
                If Debugger.IsAttached Then
                    Debugger.Break()
                End If
                Throw
            End Try
        End Using
    End Sub


    Private Sub CreateRevisionTable()
        Using cnn As IDbConnection = MRC.GetConnection
            Try
                If cnn.State <> ConnectionState.Open Then
                    cnn.Open()
                End If

                ExecuteScriptBatches(DBSqlGenerator.GetSqlCreateSystemRevisionTable(), CType(cnn, DbConnection), Nothing, True)
            Catch ex As Exception
                If Debugger.IsAttached Then
                    Debugger.Break()
                End If
                Throw
            End Try
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

                    cmd.CommandText = DBSqlGenerator.GetSqlCheckIfSchemaExists()

                    If cmd.ExecuteScalar() Is Nothing Then
                        ExecuteScriptBatches(DBSqlGenerator.GetSqlCreateSystemSchema(), CType(cnn, DbConnection), Nothing, True)
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
            Try
                If cnn.State <> ConnectionState.Open Then
                    cnn.Open()
                End If
                ExecuteScriptBatches(DBSqlGenerator.GetSqlCreateSystemModuleTable(), CType(cnn, DbConnection), Nothing, True)
            Catch ex As Exception
                If Debugger.IsAttached Then
                    Debugger.Break()
                End If
                Throw
            End Try
        End Using
    End Sub

    Private Sub CreateCustomizationTable()
        Using cnn As IDbConnection = MRC.GetConnection
            Try
                If cnn.State <> ConnectionState.Open Then
                    cnn.Open()
                End If
                ExecuteScriptBatches(DBSqlGenerator.GetSqlCreateSystemCustomizationTable(), CType(cnn, DbConnection), Nothing, True)
            Catch ex As Exception
                If Debugger.IsAttached Then
                    Debugger.Break()
                End If
                Throw
            End Try
        End Using
    End Sub

#End Region

#End Region

End Class
