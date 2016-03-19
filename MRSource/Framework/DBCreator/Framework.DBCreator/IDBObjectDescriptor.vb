Public Interface IDBObjectDescriptor
    'Function GetSqlCreate(dBObject As IDBObject) As String
    'Function GetSqlModify(dBObject As IDBObject) As String
    'Function GetSqlDelete(dBObject As IDBObject) As String
    Function GetDBObjectInstance() As IDBObject
End Interface
