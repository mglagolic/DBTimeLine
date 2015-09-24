Imports MRFramework
Imports MRFramework.MRCore

Namespace MRCopierFactory
    Public Module MRCopierFactory
        Public Function GetCopier(Of T As Class)() As MRCopier(Of T)
            Dim ret As MRCopier(Of T) = Nothing
            If GetType(T) Is GetType(System.Data.Common.DbParameter) Then
                ret = New MRDbParameterCopier(Of T)
            Else
                ret = MRCore.MRCopierFactory.GetCopier(Of T)()
            End If
            Return ret
        End Function

    End Module
End Namespace