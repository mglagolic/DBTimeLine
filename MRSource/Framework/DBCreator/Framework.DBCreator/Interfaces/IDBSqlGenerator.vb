Public Interface IDBSqlGenerator
    Function GetSqlCreateSchema(name As String) As String
    Function GetSqlDeleteSchema(name As String) As String

    Function GetSqlCreateTable(table As IDBTable) As String
    Function GetSqlDeleteTable(schemaName As String, name As String) As String

    Function GetSqlCreateField(field As IDBField) As String
    Function GetSqlModifyField(field As IDBField) As String
    Function GetSqlDeleteField(field As IDBField) As String
    Function GetFieldTypeSql(descriptor As IDBFieldDescriptor) As String
End Interface
