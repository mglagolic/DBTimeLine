Imports Framework.DBCreator

Public Class DBSchema
    Inherits DBObject
    Implements IDBSchema

    Public Sub New()

    End Sub

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.Schema
        End Get
    End Property

    Public Function AddTable(tableName As String, descriptor As IDBTableDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBTable Implements IDBSchema.AddTable
        Return MyBase.AddDBObject(tableName, descriptor, createRevision)
    End Function

    Public Function AddView(viewName As String, descriptor As IDBViewDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBView Implements IDBSchema.AddView
        Return MyBase.AddDBObject(viewName, descriptor, createRevision)
    End Function
End Class
