Option Strict On

Public Enum eDBRevisionType
    Create = 0
    Modify = 1
    Delete = 2
    AlwaysExecute = 3
End Enum

Public Class DBRevision

    Public Property DBRevisionType As eDBRevisionType
    Public Property Parent As IDBObject
    Public Property Created As Date
    Public Property Granulation As Integer
    Public Property DBObject As IDBObject
    Private Sub New()

    End Sub
    Public Sub New(created As Date, granulation As Integer, ByVal dBRevisionType As eDBRevisionType)
        MyClass.New()
        Me.DBRevisionType = dBRevisionType
        Me.Created = created

        Me.Granulation = granulation
    End Sub

    Public Sub New(revision As DBRevision)
        MyClass.New(revision.Created, revision.Granulation, revision.DBRevisionType)
    End Sub

End Class
