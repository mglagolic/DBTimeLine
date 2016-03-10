Imports MRFramework.MRDBCreator

Public MustInherit Class DBModule
    Implements IDBModule

    Private ReadOnly Property _Schemas As New List(Of DBSchema)
    Public ReadOnly Property Schemas As List(Of DBSchema) Implements IDBModule.Schemas
        Get
            Return _Schemas
        End Get
    End Property

    MustOverride Sub Create() Implements IDBModule.Create

    Protected Function AddSchema(schema As DBSchema, createRevision As DBRevision) As DBSchema
        Me.Schemas.Add(schema)
        schema.AddRevision(createRevision)

        Return schema
    End Function
End Class
