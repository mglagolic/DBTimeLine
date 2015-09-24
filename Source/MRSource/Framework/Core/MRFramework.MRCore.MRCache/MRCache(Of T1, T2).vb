' multithreaded singleton without volatile read
' used for caching

Namespace Cache

    Public Class MRCache(Of T1, T2)
        Implements IMRCache(Of T1, T2)

        Private Shared _instance As MRCache(Of T1, T2) = Nothing
        Private Shared syncRoot As New Object

        Private Sub New()

        End Sub

        Public Shared Function GetInstance() As MRCache(Of T1, T2)
            SyncLock syncRoot
                If _instance Is Nothing Then
                    _instance = New MRCache(Of T1, T2)
                End If
                Return _instance
            End SyncLock
        End Function

        Private _Container As New Dictionary(Of T1, T2)
        Private ReadOnly Property Container As Dictionary(Of T1, T2)
            Get
                Return _Container
            End Get
        End Property


        Public Sub Push(key As T1, value As T2) Implements IMRCache(Of T1, T2).Push
            SyncLock syncRoot
                If Not Me.ContainsKey(key) Then
                    Me.Container.Add(key, value)
                End If
            End SyncLock
        End Sub

        Public Function Pop(key As T1) As T2 Implements IMRCache(Of T1, T2).Pop
            SyncLock syncRoot
                Return Me.Container(key)
            End SyncLock

        End Function

        Public ReadOnly Property ContainsKey(key As T1) As Boolean Implements IMRCache(Of T1, T2).ContainsKey
            Get
                SyncLock syncRoot
                    Return Me.Container.ContainsKey(key)
                End SyncLock
            End Get
        End Property

        Public Sub Clear() Implements IMRCache(Of T1, T2).Clear
            SyncLock syncRoot
                Me.Container.Clear()
            End SyncLock
        End Sub

    End Class

End Namespace