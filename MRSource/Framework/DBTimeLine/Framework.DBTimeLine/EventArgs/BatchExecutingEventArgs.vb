Imports System.ComponentModel

Public Class BatchExecutingEventArgs
    Inherits CancelEventArgs

    Public Property DbSqlRevisions As List(Of DBSqlRevision)
    Public Property Sql As String
End Class
