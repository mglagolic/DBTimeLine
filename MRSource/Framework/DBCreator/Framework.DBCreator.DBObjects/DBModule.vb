Imports Framework.DBCreator

Public MustInherit Class DBModule
    Implements IDBModule


    Public ReadOnly Property DBObjects As New Dictionary(Of String, IDBObject) Implements IDBParent.DBObjects
    Public Property Parent As IDBChained Implements IDBModule.Parent
    Public MustOverride ReadOnly Property ModuleKey As String Implements IDBModule.ModuleKey

    Protected Function AddSchema(schemaName As String, descriptor As IDBSchemaDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBSchema Implements IDBModule.AddSchema
        Return Helpers.AddDBObjectToParent(Me, schemaName, descriptor, createRevision)
    End Function

    MustOverride Sub CreateTimeLine()

    Public Function LoadRevisions() As Object Implements IDBModule.LoadRevisions
        Dim ret As Object = Nothing

        CreateTimeLine()

        Return ret
    End Function

End Class
