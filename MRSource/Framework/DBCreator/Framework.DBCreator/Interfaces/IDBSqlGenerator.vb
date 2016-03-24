Public Interface IDBSqlGenerator
    'Function GetSqlCreateSchema(schema As IDBSchema) As String
    'Function GetSqlDeleteSchema(schema As IDBSchema) As String

    'Function GetSqlCreateTable(table As IDBTable) As String
    'Function GetSqlDeleteTable(table As IDBTable) As String

    'Function GetSqlCreateField(field As IDBField) As String
    'Function GetSqlModifyField(field As IDBField) As String
    'Function GetSqlDeleteField(field As IDBField) As String
    Function GetSqlCreate(dbObject As IDBObject) As String
    Function GetSqlModify(dbObject As IDBObject) As String
    Function GetSqlDelete(dbObject As IDBObject) As String

    Function GetFieldTypeSql(descriptor As IDBFieldDescriptor) As String

    'Function GetSqlCreateConstraint(constraint As IDBConstraint) As String

End Interface
