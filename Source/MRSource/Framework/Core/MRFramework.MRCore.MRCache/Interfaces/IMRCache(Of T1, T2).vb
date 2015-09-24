Namespace Cache


    Public Interface IMRCache(Of T1, T2)
        Function Pop(key As T1) As T2
        Sub Push(key As T1, value As T2)
        ReadOnly Property ContainsKey(key As T1) As Boolean
        'Property Enabled As Boolean

        Sub Clear()

    End Interface

End Namespace