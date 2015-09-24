Imports MRFramework.MRPersisting.Core

Namespace MRBulkCopierFactory

    Friend Module MRBulkCopierFactory
        Public Function GetCopier(providerName As String) As IMRBulkCopier
            Dim ret As IMRBulkCopier = Nothing
            Select Case providerName
                Case "System.Data.SqlClient"
                    Return New MRSqlClientBulkCopier
                Case Else
                    Return Nothing
            End Select
            Return ret
        End Function
    End Module

End Namespace