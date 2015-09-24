Imports System.Data.Common

Public Class MRC
    Private Shared _singleton As MRC = Nothing
    Private Shared syncObject As New Object

    Public Property ProviderName As String
    Public Property ConnectionString As String

    Private Sub New()

    End Sub

    Public Shared Function GetInstance() As MRC
        SyncLock syncObject
            If _singleton Is Nothing Then
                '_singleton = New MRC(providerName, connectionString)
                _singleton = New MRC
            End If
            Return _singleton
        End SyncLock
    End Function

    Private Shared _Factory As DbProviderFactory = Nothing
    Public Shared Function GetFactory() As DbProviderFactory
        SyncLock syncObject
            If _Factory Is Nothing Then
                _Factory = DbProviderFactories.GetFactory(MRC.GetInstance().ProviderName)
            End If
            Return _Factory
        End SyncLock
        'Return DbProviderFactories.GetFactory(MRC.GetInstance().ProviderName)
    End Function
        
    Public Shared Function GetConnection() As DbConnection
        Dim cnn As DbConnection = GetFactory.CreateConnection
        cnn.ConnectionString = MRC.GetInstance().ConnectionString
        Return cnn
    End Function

    Public Shared Function GetCommand() As DbCommand
        Return GetCommand(Nothing)
    End Function

    Public Shared Function GetCommand(ByVal cnn As DbConnection) As DbCommand
        Dim cmd As DbCommand = GetFactory.CreateCommand
        cmd.Connection = cnn
        Return cmd
    End Function

    Public Shared Function GetDataAdapter() As DbDataAdapter
        Dim da As DbDataAdapter = GetFactory.CreateDataAdapter
        Return da
    End Function

    Public Shared Function GetParameter(parameterName As String, dbType As System.Data.DbType, value As Object) As System.Data.Common.DbParameter
        Using cmd As DbCommand = MRC.GetCommand
            Return GetParameter(parameterName, dbType, value, cmd)
        End Using
    End Function
    Public Shared Function GetParameter(parameterName As String, dbType As System.Data.DbType, value As Object, cmd As DbCommand) As System.Data.Common.DbParameter
        Dim p As DbParameter = Nothing
        Try
            p = cmd.CreateParameter
            With p
                .ParameterName = parameterName
                .DbType = dbType
                .Value = value
            End With
        Catch ex As Exception
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
            Throw
        End Try
        Return p
    End Function
    Public Shared Function GetCommandBuilder() As DbCommandBuilder
        Return GetFactory.CreateCommandBuilder
    End Function



End Class
