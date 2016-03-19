Option Strict On


Public Class DBRevision
    Implements IDBRevision

    Public Property DBRevisionType As eDBRevisionType Implements IDBRevision.DBRevisionType
    Public Property Parent As IDBObject Implements IDBRevision.Parent
    Public Property Created As Date Implements IDBRevision.Created
    Public Property Granulation As Integer Implements IDBRevision.Granulation
    Public Property DBObject As IDBObject Implements IDBRevision.DBObject

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
