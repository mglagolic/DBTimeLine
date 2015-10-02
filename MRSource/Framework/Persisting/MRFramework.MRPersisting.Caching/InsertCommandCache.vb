Option Strict On

Imports MRFramework.MRCore.MRCache.Cache

Imports System.Reflection
Imports System.Data.Common

Public Class InsertCommandCache

    Public Class CacheValue
        Public Property InsertStatement As String
        Public Property Parameters As DbParameter()
        Public Property IdentityColumnName As String
        Public Sub New(insertStatement As String, pars As DbParameter(), identityColumnName As String)
            Me.InsertStatement = insertStatement
            Me.Parameters = pars
            Me.IdentityColumnName = identityColumnName
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

