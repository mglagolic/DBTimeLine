Public Interface IDBObjectGenerator
    Function GetSqlCreate(dbObject As IDBObject) As String
    Function GetSqlModify(dbObject As IDBObject) As String
    Function GetSqlDelete(dbObject As IDBObject) As String
End Interface
