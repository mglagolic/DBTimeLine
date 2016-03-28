Public Interface IDBObjectGenerator
    Function GetSqlCreate(dbObject As IDBObject) As String
    Function GetSqlModify(dbObject As IDBObject) As String
    Function GetSqlDelete(dbObject As IDBObject) As String

End Interface

Public Interface IDBSqlGenerator
    Function GetSqlCreate(dbObject As IDBObject) As String
    Function GetSqlModify(dbObject As IDBObject) As String
    Function GetSqlDelete(dbObject As IDBObject) As String

    Function GetFieldTypeSql(descriptor As IDBFieldDescriptor) As String

    Property DBViewGenerator As IDBObjectGenerator
End Interface
