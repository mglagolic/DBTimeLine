Option Strict On

Imports System.Reflection
Imports System.Data.Common

Imports MRFramework.MRCore.MRCache.Cache


Public Class UpdateCommandCache

    Public Class CacheValue
        Public Property UpdateStatement As String
        Public Property Parameters As DbParameter()

        Public Property UpdateStatement_LastWins As String
        Public Property Parameters_LastWins As DbParameter()

        Public Sub New(updateStatement As String, pars As DbParameter(), updateStatement_LastWins As String, pars_LastWins As DbParameter())
            Me.UpdateStatement = updateStatement
            Me.Parameters = pars
            Me.UpdateStatement_LastWins = updateStatement_LastWins
            Me.Parameters_LastWins = pars_LastWins
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

