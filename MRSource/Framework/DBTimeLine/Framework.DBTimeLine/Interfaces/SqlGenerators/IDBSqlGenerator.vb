Public Interface IDBSqlGenerator
    Function GetSqlCreateSystemModuleTable() As String
    Function GetSqlCreateSystemSchema() As String
    Function GetSqlCreateSystemRevisionTable() As String
    Function GetSqlCreateSystemAlwaysExecutingTaskTable() As String
    Function GetSqlCheckIfSchemaExists() As String

End Interface
