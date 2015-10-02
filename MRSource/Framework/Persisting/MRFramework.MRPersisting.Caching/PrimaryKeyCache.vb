Imports System.Reflection

Imports MRFramework.MRCore.MRCache.Cache

Public Class PrimaryKeyCache

    Public Class CacheValue
        Public Property DataColumns As DataColumn()
        Public Sub New(dataColumns() As DataColumn)
            Me.DataColumns = dataColumns
        End Sub
    End Class


    Private Shared ReadOnly Property Cache As IMRCache(Of Type, CacheValue)
        Get
            Return MRCache(Of Type, CacheValue).GetInstance
        End Get
    End Property


    Public Shared Sub Clear()
        Cache.Clear()
    End Sub

    Public Shared ReadOnly Property ContainsKey(key As Type) As Boolean
        Get
            Return Cache.ContainsKey(key)
        End Get
    End Property

    Public Shared Function Pop(key As Type) As CacheValue
        Return Cache.Pop(key)
    End Function

    Public Shared Sub Push(key As Type, value As CacheValue)
        Cache.Push(key, value)
    End Sub
End Class

