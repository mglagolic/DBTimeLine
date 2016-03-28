Public Interface IDBFieldGenerator
    Inherits IDBObjectGenerator

    Function GetFieldTypeSql(descriptor As IDBFieldDescriptor) As String
End Interface

Public Interface IDBTableGenerator
    Inherits IDBObjectGenerator

    Property Parent As IDBSqlGenerator
End Interface


Public Interface IDBObjectGenerator
    Function GetSqlCreate(dbObject As IDBObject) As String
    Function GetSqlModify(dbObject As IDBObject) As String
    Function GetSqlDelete(dbObject As IDBObject) As String

End Interface

Public Interface IDBSqlGenerator
    Function GetSqlCreate(dbObject As IDBObject) As String
    Function GetSqlModify(dbObject As IDBObject) As String
    Function GetSqlDelete(dbObject As IDBObject) As String


    Property DBSchemaGenerator As IDBObjectGenerator
    Property DBViewGenerator As IDBObjectGenerator
    Property DBTableGenerator As IDBObjectGenerator
    Property DBFieldGenerator As IDBFieldGenerator

End Interface
