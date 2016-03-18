Public Interface IDBObjectDescriptor
    'Property Parent As IDBObject

    Function GetSqlCreate(dBObject As IDBObject) As String
    Function GetSqlModify(dBObject As IDBObject) As String
    Function GetSqlDelete(dBObject As IDBObject) As String
End Interface
