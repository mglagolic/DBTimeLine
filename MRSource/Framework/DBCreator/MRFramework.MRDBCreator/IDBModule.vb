Public Interface IDBModule

    ReadOnly Property DBSchemas As Dictionary(Of String, DBSchema)

    Function LoadRevisions() As Object
    'Function CreateRevisions(cnn As Common.DbConnection) As Object

End Interface
