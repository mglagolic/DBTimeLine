Imports System.Data.Common
Imports System.Threading

Public Module MRGetData

#Region "Redundant"


    '<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")> _
    'Public Function ExecuteScalar(ByVal cnn As DbConnection, ByVal sql As String) As Object
    '    Dim ret As Object = Nothing
    '    Using cmd As DbCommand = MRC.GetCommand
    '        cmd.Connection = cnn
    '        cmd.CommandText = sql
    '        Dim wasClosed As Boolean = False

    '        If cnn.State = ConnectionState.Closed Then
    '            cnn.Open()
    '            wasClosed = True
    '        End If

    '        Try
    '            ret = cmd.ExecuteScalar()
    '        Catch ex As Exception
    '            If Debugger.IsAttached Then
    '                Debugger.Break()
    '            End If
    '            Throw
    '        Finally
    '            If wasClosed Then
    '                cnn.Close()
    '            End If
    '        End Try
    '    End Using

    '    Return ret
    'End Function

#End Region

    Public Function ExecuteAdapter(ByVal sql As String, Optional ByVal da As DbDataAdapter = Nothing, Optional ByVal trn As DbTransaction = Nothing) As DataSet
        Using cnn As DbConnection = MRC.GetConnection
            Return ExecuteAdapter(cnn, sql, da, trn)
        End Using
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")> _
    Public Function ExecuteAdapter(ByVal cnn As DbConnection, ByVal sql As String, Optional ByVal da As DbDataAdapter = Nothing, Optional ByVal trn As DbTransaction = Nothing) As DataSet
        Dim ds As DataSet = Nothing
        Dim wasClosed As Boolean = False
        If cnn.State = ConnectionState.Closed Then
            cnn.Open()
            wasClosed = True
        End If

        Dim cmd As DbCommand = Nothing
        Try
            cmd = MRC.GetCommand(cnn)
            cmd.CommandText = sql
            Return ExecuteAdapter(cmd, da, trn)
        Catch ex As Exception
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
            Throw
        Finally
            If wasClosed Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
            End If
        End Try
    End Function

    Public Function ExecuteAdapter(ByVal cmd As DbCommand, Optional ByVal da As DbDataAdapter = Nothing, Optional ByVal trn As DbTransaction = Nothing) As DataSet
        Dim ds As New DataSet
        Dim wasNothingAdapter As Boolean = False
        If da Is Nothing Then
            da = MRC.GetDataAdapter
            wasNothingAdapter = True
        End If

        If trn IsNot Nothing Then
            cmd.Transaction = trn
        End If
        da.SelectCommand = cmd

        Try
            da.Fill(ds)
        Catch ex As Exception
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
            Throw
        Finally
            If wasNothingAdapter Then
                If da IsNot Nothing Then
                    da.Dispose()
                    da = Nothing
                End If
            End If
        End Try
        Return ds
    End Function

    Public Function ExecuteReader(ByVal cmd As DbCommand, Optional ByVal trn As DbTransaction = Nothing) As DbDataReader
        If cmd.Transaction Is Nothing Then
            cmd.Transaction = trn
        End If
        Dim reader As DbDataReader = cmd.ExecuteReader()
        Return reader
    End Function

    Public Function ExecuteAdapterFromReader(ByVal sql As String) As DataTable
        Using cnn As DbConnection = MRC.GetConnection
            Return ExecuteAdapterFromReader(cnn, sql, Nothing)
        End Using
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Public Function ExecuteAdapterFromReader(ByVal cnn As DbConnection, ByVal sql As String, Optional ByVal trn As DbTransaction = Nothing) As DataTable
        Dim cmd As DbCommand = Nothing
        Dim dtRet As DataTable = Nothing
        Try
            cmd = MRC.GetCommand(cnn)
            cmd.CommandText = sql
            dtRet = ExecuteAdapterFromReader(cmd, trn)
        Catch e As Exception
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
            Throw
        End Try
        Return dtRet
    End Function

    Public Function ExecuteAdapterFromReader(ByVal cmd As DbCommand, Optional ByVal trn As DbTransaction = Nothing) As DataTable
        Using reader As DbDataReader = ExecuteReader(cmd, trn)
            Dim dt As New DataTable
            dt.Load(reader, LoadOption.OverwriteChanges)
            Return dt
        End Using
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Public Function GetCommandFromSql(sql As String, cnn As DbConnection) As DbCommand
        Dim cmd As DbCommand = MRC.GetCommand(cnn)
        cmd.CommandText = sql

        Return cmd
    End Function

    Public Function ExecuteScalar(ByVal cmd As DbCommand, Optional ByVal trn As DbTransaction = Nothing) As Object
        Dim ret As Object = Nothing
        Try
            If cmd.Transaction Is Nothing Then
                cmd.Transaction = trn
            End If
            cmd.ExecuteScalar()
        Catch ex As Exception
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
            Throw
        End Try
        Return ret
    End Function

End Module
