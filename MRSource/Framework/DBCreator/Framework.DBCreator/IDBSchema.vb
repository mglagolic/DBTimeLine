Public Interface IDBSchemaCommon

End Interface
Public Interface IDBSchema
    Inherits IDBSchemaCommon
    Inherits IDBObject

    Function AddTable(tableName As String, descriptor As IDBTableDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBTable
End Interface

