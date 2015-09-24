Public Class MRGenericCopier(Of T)
    Inherits MRCopier(Of T)

    Public Sub New()


    End Sub

    Public Overrides Function Copy(obj As Object) As Object
        Throw New Exception("not supported")
        Return Nothing
    End Function
End Class
