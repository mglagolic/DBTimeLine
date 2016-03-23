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
        Return Helpers.AddDBObjectToParent(Me, tableName, descriptor, createRevision)
    End Function

End Class
