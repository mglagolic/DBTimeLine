Imports MRFramework.MRDBCreator
Public Class Common
    Inherits DBModule

#Region "Singleton"

    Private Shared _singleton As IDBModule = Nothing
    Private Shared syncObject As New Object

    Private Sub New()
        MyBase.New()
    End Sub

    Public Shared Function GetInstance() As IDBModule
        SyncLock syncObject
            If _singleton Is Nothing Then
                _singleton = New Common
            End If
            Return _singleton
        End SyncLock
    End Function

#End Region

    Public Overrides Sub Create()
        With Me.AddSchema(New DBSchema("Common"),
                          New DBRevision(eDBRevisionType.Create, 2016, 3, 10, 0))

            With .AddTable(New DBTable("Revision"),
                           New DBRevision(eDBRevisionType.Create, 2016, 3, 10, 0))


                .AddField(New DBField("ID", eFieldType.guid),
                          New DBRevision(eDBRevisionType.Create, 2016, 3, 10, 0))

            End With
        End With

    End Sub

End Class
