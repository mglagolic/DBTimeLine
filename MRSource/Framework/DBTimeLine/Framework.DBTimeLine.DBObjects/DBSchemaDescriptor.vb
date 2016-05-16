Public Class DBSchemaDescriptor
    Implements IDBSchemaDescriptor

    Public Overridable Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBSchema() With {.Parent = parent, .Descriptor = Me}
    End Function

End Class

