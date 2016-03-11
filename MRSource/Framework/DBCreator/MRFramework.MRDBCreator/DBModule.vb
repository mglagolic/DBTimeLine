Imports MRFramework.MRDBCreator
Imports MRFramework.MRPersisting.Factory

Public MustInherit Class DBModule
    Implements IDBModule

    Private ReadOnly Property _DBSchemas As New List(Of DBSchema)
    Public ReadOnly Property DBSchemas As List(Of DBSchema) Implements IDBModule.DBSchemas
        Get
            Return _DBSchemas
        End Get
    End Property

    Protected Function AddSchema(schema As DBSchema, createRevision As DBRevision) As DBSchema
        DBSchemas.Add(schema)
        schema.AddRevision(createRevision)

        Return schema
    End Function

    MustOverride Sub Create()

    Public Function CreateRevisions(cnn As Common.DbConnection) As Object Implements IDBModule.CreateRevisions

        Return Nothing
    End Function

    Public Function LoadRevisions() As Object Implements IDBModule.LoadRevisions
        Dim ret As Object = Nothing

        Create()

        Return ret
    End Function







    'MustOverride Function CreateInMemoryRevisions() As List(Of DBSqlRevision) Implements IDBModule.CreateInMemoryRevisions
    'MustOverride Function LoadAlreadyExecutedRevisions() As List(Of DBSqlRevision) Implements IDBModule.LoadAlreadyExecutedRevisions
    'MustOverride Function GetNewRevisions() As List(Of DBSqlRevision) Implements IDBModule.GetNewRevisions

End Class
