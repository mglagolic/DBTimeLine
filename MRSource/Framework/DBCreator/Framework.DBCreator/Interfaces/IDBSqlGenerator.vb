Public Interface IDBSqlGenerator
    Function GetSqlCreateSchema(name As String) As String
    Function GetSqlDeleteSchema(name As String) As String

    Function GetSqlCreateTable(schemaName As String, name As String, descriptor As IDBTableDescriptor) As String
    Function GetSqlDeleteTable(schemaName As String, name As String) As String

    Function GetSqlCreateField(schemaName As String, tableName As String, name As String, descriptor As IDBFieldDescriptor) As String
    Function GetSqlModifyField(schemaName As String, tableName As String, name As String, descriptor As IDBFieldDescriptor) As String
    Function GetSqlDeleteField(schemaName As String, tableName As String, name As String) As String
    Function GetFieldTypeSql(descriptor As IDBFieldDescriptor) As String
End Interface
