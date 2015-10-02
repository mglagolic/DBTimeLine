Imports System.Reflection
Imports MRFramework.MRCore.MRCache.Cache

Public Class BaseQuerySchemaCache

    Public Class CacheValue
        Public Property SchemaTable As DataTable
        Public Sub New(schemaTable As DataTable)
            Me.SchemaTable = schemaTable
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
