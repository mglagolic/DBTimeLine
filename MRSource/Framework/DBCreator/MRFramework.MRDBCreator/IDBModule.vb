Public Interface IDBModule

    ReadOnly Property DBSchemas As List(Of DBSchema)

    Function DBCreate()

    'Function LoadAlreadyExecutedRevisions() As List(Of DBSqlRevision)
    'Function GetNewRevisions() As List(Of DBSqlRevision)

End Interface
