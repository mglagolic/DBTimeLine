Public Class BatchExecutedEventArgs
    Inherits EventArgs

    Public Property ResultType As eBatchExecutionResultType = eBatchExecutionResultType.Success
    Public Property Sql As String
    Public Property Duration As TimeSpan
    Public Property ErrorMessage As String
<<<<<<< HEAD
    'Public Property ExecutedRevisionsCount As Integer
    'Public Property TotalRevisionsCount As Integer
=======
>>>>>>> 436cc56d573645d037260128181d0217886def4b
    Public Property Exception As Exception
End Class


