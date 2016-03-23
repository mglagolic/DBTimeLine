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

    Public Overrides Function GetSqlModify() As String Implements IDBObject.GetSqlModify
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetSqlDelete() As String Implements IDBObject.GetSqlDelete
        Return DBCreator.DBSqlGenerator.GetSqlCreateSchema(Name)
    End Function
End Class
