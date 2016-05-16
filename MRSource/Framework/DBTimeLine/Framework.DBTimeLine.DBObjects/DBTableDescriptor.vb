Public Class DBTableDescriptor
    Implements IDBTableDescriptor

    Public Overridable Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBTable(Me) With {.Parent = parent}
    End Function

    Public Property CreatorFieldDescriptor As IDBFieldDescriptor Implements IDBTableDescriptor.CreatorFieldDescriptor
    Public Property CreatorFieldName As String Implements IDBTableDescriptor.CreatorFieldName

End Class

