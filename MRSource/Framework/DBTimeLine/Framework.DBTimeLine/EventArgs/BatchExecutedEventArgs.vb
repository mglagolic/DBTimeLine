Public Class BatchExecutedEventArgs
    Inherits EventArgs

    Public Property ResultType As eBatchExecutionResultType = eBatchExecutionResultType.Success
    Public Property Sql As String
    Public Property Duration As TimeSpan
    Public Property ErrorMessage As String
    Public Property ExecutedRevisionsCount As Integer
    Public Property TotalRevisionsCount As Integer
    Public Property Exception As Exception
End Class
