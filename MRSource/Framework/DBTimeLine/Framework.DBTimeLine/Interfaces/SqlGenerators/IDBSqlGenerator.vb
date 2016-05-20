Public Interface IDBSqlGenerator

    Function GetSqlCreateSystemModuleTable() As String
    Function GetSqlCreateSystemSchema() As String
    Function GetSqlCreateSystemRevisionTable() As String
    Function GetSqlCreateSystemAlwaysExecutingTaskTable() As String
    Function GetSqlCheckIfSchemaExists() As String
    Function SplitSqlStatements(sqlScript As String) As IEnumerable(Of String)

End Interface
