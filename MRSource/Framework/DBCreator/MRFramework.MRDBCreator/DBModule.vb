﻿Imports MRFramework.MRDBCreator

Public MustInherit Class DBModule
    Implements IDBModule
    Implements IDBChained

    Private ReadOnly Property _DBSchemas As New Dictionary(Of String, DBSchema)
    Public ReadOnly Property DBSchemas As Dictionary(Of String, DBSchema) Implements IDBModule.DBSchemas
        Get
            Return _DBSchemas
        End Get
    End Property

    Public Property Parent As IDBChained Implements IDBChained.Parent

    Protected Function AddSchema(schemaName As String, descriptor As DBSchemaDescriptor, Optional createRevision As DBRevision = Nothing) As DBSchema
        If Not DBSchemas.ContainsKey(schemaName) Then
            DBSchemas.Add(schemaName, New DBSchema(schemaName, descriptor) With {.Name = schemaName, .Parent = Me})
        End If
        Dim schema As DBSchema = DBSchemas(schemaName)

        If createRevision IsNot Nothing Then
            schema.AddRevision(createRevision)
        End If

        Return schema
    End Function

    MustOverride Sub CreateTimeLine()

    Public Function CreateRevisions(cnn As Common.DbConnection) As Object Implements IDBModule.CreateRevisions

        Return Nothing
    End Function

    Public Function LoadRevisions() As Object Implements IDBModule.LoadRevisions
        Dim ret As Object = Nothing

        CreateTimeLine()

        Return ret
    End Function

End Class
