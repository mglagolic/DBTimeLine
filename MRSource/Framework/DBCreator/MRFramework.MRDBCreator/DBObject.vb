Imports MRFramework.MRDBCreator

Public MustInherit Class DBObject
    Implements IDBObject

#Region "IDBObject"
    Public Property Name As String Implements IDBObject.Name

    Public Property Parent As IDBObject Implements IDBObject.Parent

    Private ReadOnly Property _Revisions As New List(Of DBRevision)
    Public ReadOnly Property Revisions As List(Of DBRevision) Implements IDBObject.Revisions
        Get
            Return _Revisions
        End Get
    End Property

    Public MustOverride ReadOnly Property DBObjectType As eDBObjectTypes Implements IDBObject.DBObjectType

    Public Function AddRevision(revision As DBRevision) As DBRevision Implements IDBObject.AddRevision
        Me.Revisions.Add(revision)
        revision.Parent = Me

        Return revision
    End Function

#End Region

End Class
