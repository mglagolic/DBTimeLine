Public Class DBSchemaDescriptor
    Implements IDBSchemaDescriptor

    Public Overridable Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBSchema(Me) With {.Parent = parent}
    End Function

End Class

