Public Interface IDBSqlGenerator
    Function GetSqlCreate(dbObject As IDBObject) As String
    Function GetSqlModify(dbObject As IDBObject) As String
    Function GetSqlDelete(dbObject As IDBObject) As String


    Property DBSchemaGenerator As IDBObjectGenerator
    Property DBViewGenerator As IDBObjectGenerator
    Property DBTableGenerator As IDBObjectGenerator
    Property DBFieldGenerator As IDBFieldGenerator
    Property DBPrimaryKeyConstraintGenerator As IDBObjectGenerator
    Property DBForeignKeyConstraintGenerator As IDBObjectGenerator
    Property DBIndexGenerator As IDBIndexGenerator

End Interface
