Public Class BatchExecutedEventArgs
    Inherits EventArgs

    Public Property ResultType As eBatchExecutionResultType = eBatchExecutionResultType.Success
    Public Property Sql As String
    Public Property Duration As TimeSpan
    Public Property ErrorMessage As String

End Class
