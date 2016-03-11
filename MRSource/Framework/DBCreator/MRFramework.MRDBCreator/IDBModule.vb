Public Interface IDBModule

    ReadOnly Property DBSchemas As List(Of DBSchema)

    Function DBCreate(cnn As Common.DbConnection)

End Interface
