Option Strict On

Imports MRFramework.MRCore.Enums
Imports System.Data.Common
Imports MRFramework.MRPersisting.Cache
Imports MRFramework.MRPersisting.Factory
Imports MRFramework.MRPersisting.Core
Imports MRFramework.MRCore

Public MustInherit Class MRPersister
    Implements IMRPersister
    Implements IDisposable


    Public Sub New()

    End Sub

    Public Property CNN As Common.DbConnection Implements IMRPersister.CNN

#Region "DataReaderPaging"


#Region "Overridable Properties"
    Public MustOverride ReadOnly Property DataBaseTableName As String
    Protected Friend Overridable Property SYS_GUID As String = MRCore.Globals.SYS_GUID

    Public Overridable ReadOnly Property SQL As String
        Get
            Return "select * from " & DataBaseTableName
        End Get
    End Property

    Public Property Where As String Implements IMRPersister.Where


    Private _ManualPrimaryKey As Boolean = False
    Protected Friend Overridable Property ManualPrimaryKey As Boolean
        Get
            Return _ManualPrimaryKey
        End Get
        Set(value As Boolean)
            _ManualPrimaryKey = value
        End Set
    End Property

#End Region

#Region "Primary key and schema"

    Private _PrimaryKey() As DataColumn
    Protected Friend Property PrimaryKey() As DataColumn()
        Get
            Return _PrimaryKey
        End Get
        Set(value As DataColumn())
            _PrimaryKey = value
        End Set
    End Property

    Private Function GetPrimaryKeyFromSchema(schemaTable As DataTable) As DataColumn()
        Dim ret() As DataColumn = Nothing
        Dim drows() As DataRow = schemaTable.Select("ISKEY = 1 AND ISHIDDEN = 0")

        If drows IsNot Nothing AndAlso drows.Length > 0 Then
            ReDim ret(drows.Length - 1)
            For i As Integer = 0 To drows.Length - 1
                ret(i) = New DataColumn(drows(i)("COLUMNNAME").ToString)
            Next
        End If
        Return ret
    End Function

#Region "Cache"
    Private _PrimaryKeyCached As DataColumn() = Nothing
    Private Function GetPrimaryKey() As DataColumn()
        If _PrimaryKeyCached Is Nothing Then
            If Not Cache.PrimaryKeyCache.ContainsKey(Me.GetType) Then
                Dim query As String = Me.GetQuery_SQL(Me.SQL, Me.Where)
                Dim schema As DataTable = GetSchema(query)
                Cache.PrimaryKeyCache.Push(Me.GetType, New Cache.PrimaryKeyCache.CacheValue(GetPrimaryKeyFromSchema(schema)))
            End If
            _PrimaryKeyCached = Cache.PrimaryKeyCache.Pop(Me.GetType).DataColumns
        End If
        Return _PrimaryKeyCached
    End Function

#End Region

    Protected Friend Function GetSchema(query As String) As DataTable
        Dim schema As DataTable = Nothing

        Using lookupCnn As DbConnection = MRC.GetConnection
            Using cmd As DbCommand = GetCommandFromSql(query, lookupCnn)
                Try
                    lookupCnn.Open()
                    Using reader As DbDataReader = cmd.ExecuteReader(CommandBehavior.Default Or CommandBehavior.KeyInfo Or CommandBehavior.SchemaOnly)
                        schema = reader.GetSchemaTable

                        If Not reader.IsClosed Then
                            reader.Close()
                        End If
                    End Using
                Catch ex As Exception
                    If Debugger.IsAttached Then
                        Debugger.Break()
                    End If
                    Throw
                Finally
                    If lookupCnn IsNot Nothing Then
                        If lookupCnn.State = ConnectionState.Open Then
                            lookupCnn.Close()
                        End If
                    End If
                End Try
            End Using
        End Using
        Return schema
    End Function

    Private Sub AddPrimaryKeyToOrder()

        For Each pk As DataColumn In Me.PrimaryKey
            Dim exists As Boolean = False
            For Each oi As MRCore.IMROrderItem In Me.OrderItems
                If pk.ColumnName.ToUpper.Equals(oi.ColumnName.ToUpper) Then
                    exists = True
                    Exit For
                End If
            Next
            If Not exists Then
                Me.OrderItems.Add(New MROrderItem(pk.ColumnName, MRCore.Enums.eOrderDirection.Ascending))

            End If
        Next

    End Sub

    Private Sub SetPrimaryKey()
        If Not Me.ManualPrimaryKey Then
            If Me.PrimaryKey Is Nothing OrElse Me.PrimaryKey.Length = 0 Then
                Me.PrimaryKey = GetPrimaryKey()
            End If
        End If
    End Sub

#End Region

#Region "IMDataReader"

#Region "Properties"


    Private _OrderItems As New List(Of MRCore.IMROrderItem)
    Public ReadOnly Property OrderItems As List(Of IMROrderItem) Implements IMRPersister.OrderItems
        Get
            Return _OrderItems
        End Get
    End Property


#End Region

#End Region

#Region "IMRDataPaging2"

#Region "Properties"
    Private _PageSize As Integer = MRCore.Globals.PageSize
    Public Overridable Property PageSize As Integer Implements IMRPersister.PageSize
        Get
            Return _PageSize
        End Get
        Set(value As Integer)
            _PageSize = value
        End Set
    End Property
    Private _PagingEnabled As Boolean = MRCore.Globals.PagingEnabled
    Public Overridable Property PagingEnabled As Boolean Implements IMRPersister.PagingEnabled
        Get
            Return _PagingEnabled
        End Get
        Set(value As Boolean)
            _PagingEnabled = value
        End Set
    End Property
#End Region

#End Region

#Region "Getting Data"

    Public Event RowReading(sender As Object, e As IMRRowReadingEventArgs) Implements IMRPersister.RowReading
    Public Event RowRead(sender As Object, e As IMRRowReadEventArgs) Implements IMRPersister.RowRead
    Public Overridable Sub OnRowReading(sender As Object, e As IMRRowReadingEventArgs)
        RaiseEvent RowReading(Me, e)
    End Sub
    Public Overridable Sub OnRowRead(sender As Object, e As IMRRowReadEventArgs)
        RaiseEvent RowRead(Me, e)
    End Sub
#Region "Private methods"

#Region "old"
    'Private Function GetReadableRows(query As String, Optional transaction As DbTransaction = Nothing) As List(Of IMRReadableObject)
    '    Dim ret As New List(Of IMRReadableObject)
    '    Using cmd As DbCommand = MRGetData.GetCommandFromSql(query, Me.CNN)
    '        Using reader As DbDataReader = ExecuteReader(cmd, transaction)
    '            While reader.Read
    '                Dim readableObject As New MRReadableObject
    '                For i As Integer = 0 To reader.FieldCount - 1
    '                    readableObject.ReadableDataColumns.Add(New MRReadableDataColumn(reader.GetName(i).ToUpper, reader.GetValue(i)))
    '                Next
    '                ret.Add(readableObject)
    '            End While
    '            If Not reader.IsClosed Then
    '                reader.Close()
    '            End If
    '        End Using
    '    End Using
    '    Return ret
    'End Function
#End Region

    Private Function ReadingData(query As String, Optional transaction As DbTransaction = Nothing) As Dictionary(Of Object, IMRDLO)
        Dim ret As New Dictionary(Of Object, IMRDLO)
        Using cmd As DbCommand = MRGetData.GetCommandFromSql(query, Me.CNN)
            Using reader As DbDataReader = ExecuteReader(cmd, transaction)
                Dim rowIndex As Long = 0
                While reader.Read
                    Dim ce As New MREventArgs.MRRowReadingEventArgs(False, rowIndex)
                    OnRowReading(Me, ce)

                    If Not ce.Cancel Then
                        Dim dlo As New MRDLO
                        For i As Integer = 0 To reader.FieldCount - 1
                            dlo.ColumnValues.Add(reader.GetName(i), reader.GetValue(i))
                        Next
                        OnRowRead(Me, New MREventArgs.MRRowReadEventArgs(rowIndex, dlo))
                        ret.Add(dlo.ColumnValues(PrimaryKey(0).ColumnName), dlo)
                    End If
                    rowIndex += 1
                End While
                If Not reader.IsClosed Then
                    reader.Close()
                End If
            End Using
        End Using

        Return ret
    End Function
#End Region

    Public Function GetTotalRowCount(Optional transaction As DbTransaction = Nothing) As Integer Implements IMRPersister.GetTotalRowCount
        Dim ret As Integer = 0
        SetPrimaryKey()
        AddPrimaryKeyToOrder()
        Dim queryFillAll As String = Me.GetQuery_FillAll( _
                                                        Me.GetQuery_SQL(Me.SQL, Me.Where), _
                                                        Me.GetQuery_RowNumberClause(Me.OrderItems), _
                                                        Me.PrimaryKey _
                                                        )

        Dim queryTotalRowCount As String = Me.GetQuery_TotalRowCount(queryFillAll)
        Dim cmd As DbCommand = GetCommandFromSql(queryTotalRowCount, Me.CNN)
        cmd.Transaction = transaction
        Dim retO As Object = cmd.ExecuteScalar
        If TypeOf retO Is Integer Then
            ret = CInt(retO)
        End If

        Return ret
    End Function

    Private Const IncludeRowNumInQueries As Boolean = True


#Region "QueryBuilder"

    Protected Overridable Function GetQuery_SQL(baseSql As String, where As String) As String
        Return MRQueryBuilder.GetSQL(baseSql, where)
    End Function
    Protected Overridable Function GetQuery_RowNumberClause(orderItems As List(Of IMROrderItem)) As String
        Return MRQueryBuilder.GetRowNumberClause(orderItems)
    End Function
    Protected Overridable Function GetQuery_FillAll(SQLClause As String, rowNumberClause As String, primaryKey() As DataColumn, Optional ByVal includeRowNum As Boolean = IncludeRowNumInQueries) As String
        Return MRQueryBuilder.GetQuery_FillAll(SQLClause, rowNumberClause, primaryKey, includeRowNum)
    End Function

    Public Overridable Function GetQuery_MainOrderBy(fillAllQuery As String) As String
        Return MRQueryBuilder.GetQuery_MainOrderBy(fillAllQuery)
    End Function

    Public Overridable Function GetQuery_Page(queryFillAll As String, pageIndex As Integer, pageSize As Integer, totalRowCount As Integer, pagingEnabled As Boolean) As String
        Return MRQueryBuilder.GetQuery_Page(queryFillAll, pageIndex, pageSize, totalRowCount, pagingEnabled)
    End Function
    Public Overridable Function GetQuery_TotalRowCount(getQuery_FillAllClause As String) As String
        Return MRQueryBuilder.GetQuery_TotalRowCount(getQuery_FillAllClause)
    End Function
#End Region

    Public Function GetData(Optional transaction As DbTransaction = Nothing) As Dictionary(Of Object, IMRDLO) Implements IMRPersister.GetData
        SetPrimaryKey()
        AddPrimaryKeyToOrder()
        Dim queryFillAll As String = Me.GetQuery_FillAll( _
             Me.GetQuery_SQL(Me.SQL, Me.Where), _
             Me.GetQuery_RowNumberClause(Me.OrderItems), _
             Me.PrimaryKey _
             )
        Dim query As String = Me.GetQuery_MainOrderBy(queryFillAll)

        Return ReadingData(query, transaction)
    End Function

    Public Function GetDataPage(pageIndex As Integer, Optional transaction As DbTransaction = Nothing) As Dictionary(Of Object, IMRDLO) Implements IMRDataPaging.GetDataPage
        SetPrimaryKey()
        AddPrimaryKeyToOrder()
        Dim totalRowCount As Integer = Me.GetTotalRowCount(transaction)

        Dim queryFillAll As String = Me.GetQuery_FillAll( _
             Me.GetQuery_SQL(Me.SQL, Me.Where), _
             Me.GetQuery_RowNumberClause(Me.OrderItems), _
             Me.PrimaryKey _
             )
        Dim queryPage As String = Me.GetQuery_Page(queryFillAll, pageIndex, Me.PageSize, totalRowCount, Me.PagingEnabled)
        Dim query As String = Me.GetQuery_MainOrderBy(queryPage)

        Return ReadingData(query, transaction)
    End Function

    Public Sub GettingData(Optional transaction As DbTransaction = Nothing) Implements IMRPersister.GettingData
        SetPrimaryKey()
        AddPrimaryKeyToOrder()
        Dim queryFillAll As String = Me.GetQuery_FillAll( _
             Me.GetQuery_SQL(Me.SQL, Me.Where), _
             Me.GetQuery_RowNumberClause(Me.OrderItems), _
             Me.PrimaryKey _
             )
        Dim query As String = Me.GetQuery_MainOrderBy(queryFillAll)

        ReadingData(query, transaction)
    End Sub

    Public Sub GettingDataPage(pageIndex As Integer, Optional transaction As DbTransaction = Nothing) Implements IMRPersister.GettingDataPage
        SetPrimaryKey()
        AddPrimaryKeyToOrder()
        Dim totalRowCount As Integer = Me.GetTotalRowCount(transaction)

        Dim queryFillAll As String = Me.GetQuery_FillAll( _
             Me.GetQuery_SQL(Me.SQL, Me.Where), _
             Me.GetQuery_RowNumberClause(Me.OrderItems), _
             Me.PrimaryKey _
             )
        Dim queryPage As String = Me.GetQuery_Page(queryFillAll, pageIndex, Me.PageSize, totalRowCount, Me.PagingEnabled)
        Dim query As String = Me.GetQuery_MainOrderBy(queryPage)

        ReadingData(query, transaction)
    End Sub

#End Region

#End Region


#Region "DataWriter"

    Public Overridable ReadOnly Property SQLBase As String
        Get
            Return "select * from " & Me.DataBaseTableName
        End Get
    End Property


#Region "Inserting"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="lsDlo"></param>
    ''' <param name="transaction"></param>
    ''' <returns></returns>
    ''' <remarks>Nema vraćanja identity valuesa... Treba doraditi dizajn baze, stagging tables.</remarks>
    Public Function InsertBulk(lsDlo As List(Of IMRDLO), transaction As DbTransaction) As List(Of IMRInsertDLOReturnValue) Implements IMRPersister.InsertBulk
        Dim ret As List(Of IMRInsertDLOReturnValue) = Nothing

        Dim sqlBulkCopier As IMRBulkCopier = MRBulkCopierFactory.GetCopier(MRC.GetInstance.ProviderName)
        If sqlBulkCopier Is Nothing Then
            ' odi na pojedinacni insert
            For Each dlo As MRDLO In lsDlo
                Insert(dlo, transaction)
            Next
        Else
            Try
                'Dim schemaTable As DataTable = Helpers.GetCacheValueSchemaTable(Me).SchemaTable

                With sqlBulkCopier
                    .CNN = Me.CNN
                    .Transaction = transaction
                    .SchemaTable = SchemaTable
                    .DestinationTableName = Me.DataBaseTableName
                End With

                Using dt As New DataTable
                    Using typeConverter As New MRPersisting.MRTypeConverter
                        For Each dr As DataRow In SchemaTable.Rows
                            'For Each dr As DataRow In schemaTable.Select("IsIdentity = 0")
                            If Not CBool(dr("IsIdentity")) Then
                                dt.Columns.Add(dr("ColumnName").ToString, typeConverter.GetDataType(dr("datatype").ToString))
                            End If
                            'If CBool(dr("IsIdentity")) Then
                            '    dt.Columns(dt.Columns.Count - 1).AutoIncrement = True
                            'End If
                        Next
                        For Each dlo As MRDLO In lsDlo
                            Dim nr As DataRow = dt.NewRow
                            For Each col As DataColumn In dt.Columns
                                'If Not col.AutoIncrement Then
                                nr(col.ColumnName) = typeConverter.GetValue(dlo.ColumnValues(col.ColumnName))
                                'End If
                            Next
                            dt.Rows.Add(nr)
                        Next
                        sqlBulkCopier.WriteToServer(dt)
                    End Using
                End Using

            Catch ex As Exception
                If Debugger.IsAttached Then
                    Debugger.Break()
                End If
                Throw
            Finally
                If sqlBulkCopier IsNot Nothing Then
                    sqlBulkCopier.Dispose()
                    sqlBulkCopier = Nothing
                End If
            End Try
        End If
        Return ret
    End Function

#Region "Cache"


    Private _schemaTable As DataTable = Nothing
    Private ReadOnly Property SchemaTable As DataTable
        Get
            If _schemaTable Is Nothing Then
                _schemaTable = Helpers.GetCacheValueSchemaTable(Me).SchemaTable
            End If
            Return _schemaTable
        End Get
    End Property

    Private _cvInsertCommand As InsertCommandCache.CacheValue = Nothing
    Private ReadOnly Property cvInsertCommand As InsertCommandCache.CacheValue
        Get
            If _cvInsertCommand Is Nothing Then
                _cvInsertCommand = Helpers.GetInsertCommandCachedValue(Me, SchemaTable)
            End If
            Return _cvInsertCommand
        End Get
    End Property

    Private _cvUpdateCommand As UpdateCommandCache.CacheValue = Nothing
    Private ReadOnly Property cvUpdateCommand As UpdateCommandCache.CacheValue
        Get
            If _cvUpdateCommand Is Nothing Then
                _cvUpdateCommand = Helpers.GetUpdateCommandCachedValue(Me, SchemaTable)
            End If
            Return _cvUpdateCommand
        End Get
    End Property

    Private _cvDeleteCommand As DeleteCommandCache.CacheValue = Nothing
    Private ReadOnly Property cvDeleteCommand As DeleteCommandCache.CacheValue
        Get
            If _cvDeleteCommand Is Nothing Then
                _cvDeleteCommand = Helpers.GetDeleteCommandCachedValue(Me, SchemaTable)
            End If
            Return _cvDeleteCommand
        End Get
    End Property

#End Region

    Public Function Insert(dlo As IMRDLO, transaction As DbTransaction) As IMRInsertDLOReturnValue Implements IMRPersister.Insert
        Dim ret As MRInsertDLOReturnValue = Nothing
        Try
            Using cmd As DbCommand = MRC.GetCommand(CNN)
                Helpers.InsertDloIntoCommand(dlo, cvInsertCommand.Parameters, cmd)

                cmd.CommandText = cvInsertCommand.InsertStatement
                cmd.Transaction = transaction
                If cvInsertCommand.IdentityColumnName IsNot Nothing Then
                    cmd.CommandText &= " SELECT cast(SCOPE_IDENTITY() as bigint)"
                End If
                If cmd.Parameters.Contains("@" & Me.SYS_GUID) Then
                    'cmd.Parameters("@" & Me.SYS_GUID).Value = Guid.NewGuid
                    cmd.Parameters("@" & Me.SYS_GUID).Value = Factory.GetNewGuid
                End If

                Dim o As Object = cmd.ExecuteScalar
                ret = New MRInsertDLOReturnValue
                ret.Dlo = CType(dlo.Clone, MRDLO)
                ret.Result = Core.Enums.eInsertDLOResults.Success
                If o IsNot Nothing Then
                    ret.Dlo.ColumnValues(cvInsertCommand.IdentityColumnName) = CLng(o)
                End If
            End Using
        Catch ex As Exception
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
            Throw
        End Try

        Return ret
    End Function

#End Region

#Region "Update"
    Private Function GetSingleDLO(primaryKeyValue As Object, pkParameter As DbParameter, cmd As DbCommand) As MRDLO
        Dim ret As MRDLO = Nothing
        Try
            Dim sql As String = Me.GetQuery_SQL(Me.SQL, Me.PrimaryKey(0).ColumnName & " = " & pkParameter.ParameterName)
            Dim copier As New MRDbParameterCopier(Of DbParameter)
            Dim input As New MRDbParameterCopierInput
            With input
                .Parameter = pkParameter
                .Command = cmd
            End With
            Dim output As MRDbParameterCopierOutput = CType(copier.Copy(input), MRDbParameterCopierOutput)
            output.Parameter.Value = primaryKeyValue
            cmd.Parameters.Clear()
            cmd.Parameters.Add(output.Parameter)
            cmd.CommandText = sql
            Using dt As DataTable = MRPersisting.Factory.ExecuteAdapterFromReader(cmd)
                If dt.Rows.Count = 1 Then
                    ' concurrency violation
                    ret = New MRDLO
                    For Each col As DataColumn In dt.Columns
                        ret.ColumnValues.Add(col.ColumnName, dt.Rows(0)(col.ColumnName))
                    Next
                ElseIf dt.Rows.Count = 0 Then
                    ' netko obrisao dlo
                Else
                    Throw New Exception("Unexpected row count", Nothing)
                End If
            End Using
        Catch ex As Exception
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
            Throw
        End Try
        Return ret
    End Function

    Public Function Update(dlo As IMRDLO, transaction As DbTransaction, Optional lastWins As Boolean = False) As IMRUpdateDLOReturnValue Implements IMRPersister.Update
        Dim ret As New MRUpdateDLOReturnValue
        ret.Result = Core.Enums.eUpdateDLOResults.Success

        SetPrimaryKey()

        Try
            Using cmd As DbCommand = MRC.GetCommand(CNN)
                If lastWins Then
                    Helpers.InsertDloIntoCommand(dlo, cvUpdateCommand.Parameters_LastWins, cmd)
                    cmd.CommandText = cvUpdateCommand.UpdateStatement_LastWins
                Else
                    Try
                        Helpers.InsertDloIntoCommand(dlo, cvUpdateCommand.Parameters, cmd)
                        cmd.CommandText = cvUpdateCommand.UpdateStatement
                    Catch ex As Exception
                        If Debugger.IsAttached Then
                            Debugger.Break()
                        End If
                    End Try

                End If

                'cmd.Parameters("@" & Me.SYS_GUID).Value = Guid.NewGuid
                If cmd.Parameters.Contains("@" & Me.SYS_GUID) Then
                    cmd.Parameters("@" & Me.SYS_GUID).Value = MRPersisting.Factory.GetNewGuid
                End If

                cmd.Transaction = transaction

                Dim cnt As Integer = cmd.ExecuteNonQuery
                If cnt = 0 Then
                    ' concurrency violation
                    Dim p As DbParameter = Helpers.GetOriginalPkParameter(Me, cmd.Parameters)
                    Dim concurrentDLO As IMRDLO = GetSingleDLO(p.Value, p, cmd)
                    If concurrentDLO IsNot Nothing Then
                        ret.Result = Core.Enums.eUpdateDLOResults.ConcurrencyViolation
                        ret.ConcurrentDLO = concurrentDLO
                    Else
                        ret.Result = Core.Enums.eUpdateDLOResults.DLODeleted
                    End If
                End If
            End Using
        Catch ex As Exception
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
            Throw
        End Try
        Return ret
    End Function

#End Region

#Region "Delete"
    Public Function Delete(dlo As IMRDLO, transaction As DbTransaction, Optional lastWins As Boolean = False) As IMRDeleteDLOReturnValue Implements IMRPersister.Delete
        Dim ret As New MRDeleteDLOReturnValue
        ret.Result = Core.Enums.eDeleteDLOResults.Success

        SetPrimaryKey()

        Try
            'Dim schemaTable As DataTable = Helpers.GetCacheValueSchemaTable(Me).SchemaTable
            'Dim cv As DeleteCommandCache.CacheValue = Helpers.GetDeleteCommandCachedValue(Me, schemaTable)

            Using cmd As DbCommand = MRC.GetCommand(CNN)
                If lastWins Then
                    Helpers.InsertDloIntoCommand(dlo, cvDeleteCommand.Parameters_LastWins, cmd)
                Else
                    Helpers.InsertDloIntoCommand(dlo, cvDeleteCommand.Parameters, cmd)
                End If

                cmd.CommandText = cvDeleteCommand.DeleteStatement
                cmd.Transaction = transaction

                Dim cnt As Integer = cmd.ExecuteNonQuery()
                If cnt = 0 Then
                    Dim p As DbParameter = Helpers.GetOriginalPkParameter(Me, cmd.Parameters)
                    Dim concurrentDLO As IMRDLO = GetSingleDLO(p.Value, p, cmd)
                    If concurrentDLO IsNot Nothing Then
                        ret.Result = Core.Enums.eDeleteDLOResults.ConcurrencyViolation
                        ret.ConcurrentDLO = concurrentDLO
                    Else
                        ret.Result = Core.Enums.eDeleteDLOResults.DLODeleted
                    End If
                End If
            End Using
        Catch ex As Exception
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
            Throw
        End Try
        Return ret
    End Function
#End Region

    Protected Friend Enum eCommandType
        Insert = 0
        Update = 1
        UpdateLastWins = 2
        Delete = 3
        DeleteLastWins = 4
    End Enum

    Private Function GetModifiedUpdateDeleteCommandText(cmdText As String, Optional lastWins As Boolean = False) As String
        Dim sql As String = cmdText
        sql = sql.Substring(0, sql.IndexOf("WHERE"))
        sql = sql & <string>
WHERE                        
    <%= Me.PrimaryKey(0).ColumnName %> = @Original_<%= Me.PrimaryKey %></string>.Value
        If (Me.SchemaTable.Select("ColumnName = '" & Me.SYS_GUID & "'").Length = 1) AndAlso Not lastWins Then
            sql &= <string> and <%= Me.SYS_GUID %> = @Original_<%= Me.SYS_GUID %></string>.Value
        End If

        Return sql
    End Function
    Protected Friend Overridable Function GetCommand(commandType As eCommandType) As DbCommand
        Dim cmd As DbCommand = Nothing

        Using da As DbDataAdapter = MRC.GetDataAdapter
            Using lookupCnn As DbConnection = MRC.GetConnection

                da.SelectCommand = MRC.GetCommand
                da.SelectCommand.CommandText = Me.SQLBase
                da.SelectCommand.Connection = lookupCnn

                Try
                    lookupCnn.Open()
                    Using builder As DbCommandBuilder = MRC.GetCommandBuilder
                        builder.DataAdapter = da
                        Select Case commandType
                            Case eCommandType.Insert
                                cmd = builder.GetInsertCommand(True)
                            Case eCommandType.Update
                                cmd = builder.GetUpdateCommand(True)
                                cmd.CommandText = GetModifiedUpdateDeleteCommandText(cmd.CommandText)
                            Case eCommandType.UpdateLastWins
                                cmd = builder.GetUpdateCommand(True)
                                cmd.CommandText = GetModifiedUpdateDeleteCommandText(cmd.CommandText, lastWins:=True)
                            Case eCommandType.Delete
                                cmd = builder.GetDeleteCommand(True)
                                cmd.CommandText = GetModifiedUpdateDeleteCommandText(cmd.CommandText)
                            Case eCommandType.DeleteLastWins
                                cmd = builder.GetDeleteCommand(True)
                                cmd.CommandText = GetModifiedUpdateDeleteCommandText(cmd.CommandText, lastWins:=True)
                            Case Else
                                Throw New Exception("Not supported")
                        End Select
                    End Using
                Catch ex As Exception
                    If Debugger.IsAttached Then
                        Debugger.Break()
                    End If
                    Throw
                Finally
                    If lookupCnn.State = ConnectionState.Open Then
                        lookupCnn.Close()
                    End If
                End Try

            End Using
        End Using

        Return cmd
    End Function

#End Region


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
            End If
        End If
        disposedValue = True
    End Sub

    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region




    
End Class
