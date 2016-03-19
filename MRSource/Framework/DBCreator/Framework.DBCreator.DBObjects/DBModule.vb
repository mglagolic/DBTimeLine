Public MustInherit Class DBModule
    Implements IDBModule

    Private ReadOnly Property _DBSchemas As New Dictionary(Of String, IDBSchema)
    Public ReadOnly Property DBSchemas As Dictionary(Of String, IDBSchema) Implements IDBModule.DBSchemas
        Get
            Return _DBSchemas
        End Get
    End Property

    Public Property Parent As IDBChained Implements IDBModule.Parent

    Protected Function AddSchema(schemaName As String, descriptor As IDBSchemaDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBSchema Implements IDBModule.AddSchema
        If Not DBSchemas.ContainsKey(schemaName) Then
            Dim newDBObject As IDBObject = descriptor.GetDBObjectInstance
            With newDBObject
                .Name = schemaName
                .Parent = Me
            End With

            DBSchemas.Add(schemaName, newDBObject)
            DirectCast(Parent, DBCreator).DBSchemas.Add(schemaName, newDBObject)
        End If
        Dim schema As DBSchema = DBSchemas(schemaName)

        If createRevision IsNot Nothing Then
            schema.AddRevision(createRevision)
        End If

        Return schema
    End Function

    MustOverride Sub CreateTimeLine()

    Public Function LoadRevisions() As Object Implements IDBModule.LoadRevisions
        Dim ret As Object = Nothing

        CreateTimeLine()

        Return ret
    End Function

End Class
