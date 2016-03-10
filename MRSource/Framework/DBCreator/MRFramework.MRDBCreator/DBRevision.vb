Public Enum eDBRevisionType
    Create = 0
    Modify = 1
    Delete = 2
    AlwaysExecute = 3
End Enum

Public Class DBRevision

    Public Property DBRevisionType As eDBRevisionType
    Public Property Parent As IDBObject

    Private Sub New()

    End Sub

    Public Sub New(ByVal dBRevisionType As eDBRevisionType, year As Integer, month As Integer, day As Integer, granula As Integer, Optional dbObject As IDBObject = Nothing)
        MyClass.New()

    End Sub


End Class
