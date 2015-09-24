'Imports MRCore
Imports MRFramework.MRPersisting.Core
Imports System.Data.Common


Public Class MRSqlClientBulkCopier
    Implements IMRBulkCopier

    Public Property DestinationTableName As String Implements IMRBulkCopier.DestinationTableName
    Public Property CNN As DbConnection Implements IMRBulkCopier.CNN
    Public Property Transaction As DbTransaction Implements IMRBulkCopier.Transaction
    Public Property SchemaTable As DataTable Implements IMRBulkCopier.SchemaTable

    Public Sub New()

    End Sub
    
    Public Sub WriteToServer(dt As DataTable) Implements IMRBulkCopier.WriteToServer
        'Using SqlBulkCopier As New SqlClient.SqlBulkCopy(DirectCast(Me.CNN, SqlClient.SqlConnection), SqlClient.SqlBulkCopyOptions.Default Or SqlClient.SqlBulkCopyOptions.CheckConstraints Or SqlClient.SqlBulkCopyOptions.FireTriggers, CType(Me.Transaction, SqlClient.SqlTransaction))
        Using SqlBulkCopier As New SqlClient.SqlBulkCopy(DirectCast(Me.CNN, SqlClient.SqlConnection), SqlClient.SqlBulkCopyOptions.CheckConstraints Or SqlClient.SqlBulkCopyOptions.FireTriggers, CType(Me.Transaction, SqlClient.SqlTransaction))
            SqlBulkCopier.DestinationTableName = Me.DestinationTableName
            For Each dr As DataRow In SchemaTable.Select("IsIdentity = 0")
                'For Each dr As DataRow In SchemaTable.Rows
                SqlBulkCopier.ColumnMappings.Add(New System.Data.SqlClient.SqlBulkCopyColumnMapping(dr("ColumnName").ToString, dr("ColumnName").ToString))
            Next

            SqlBulkCopier.WriteToServer(dt)
            SqlBulkCopier.Close()
        End Using
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            Me.CNN = Nothing
            Me.SchemaTable = Nothing
            Me.Transaction = Nothing
            Me.DestinationTableName = Nothing
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
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
