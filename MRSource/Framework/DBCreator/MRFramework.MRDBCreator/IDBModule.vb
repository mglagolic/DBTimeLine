Public Interface IDBModule

    ReadOnly Property Schemas As List(Of DBSchema)
    'Function AddSchema(schema As DBSchema, createRevision As DBRevision) As DBSchema
    Sub Create()

End Interface
