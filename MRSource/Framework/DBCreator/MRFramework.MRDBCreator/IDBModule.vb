Public Interface IDBModule

    ReadOnly Property DBSchemas As List(Of DBSchema)

    Function LoadRevisions()
    Function CreateRevisions(cnn As Common.DbConnection)

End Interface
