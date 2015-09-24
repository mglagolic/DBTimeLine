Imports System.Data.Common
Public Interface IMRBulkCopier
    Inherits IDisposable

    Property DestinationTableName As String
    Property CNN As DbConnection
    Property Transaction As DbTransaction
    Property SchemaTable As DataTable

    Sub WriteToServer(dt As DataTable)
End Interface
