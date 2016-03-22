Public Interface IDBSchemaDescriptor
    Inherits IDBObjectDescriptor
End Interface


Public Interface IDBSchema
    Inherits IDBObject

    Function AddTable(tableName As String, descriptor As IDBTableDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBTable
End Interface

