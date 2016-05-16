Public Class DBViewDescriptor
    Implements IDBViewDescriptor

    Public Property Body As String Implements IDBViewDescriptor.Body
    Public Property WithSchemaBinding As Boolean Implements IDBViewDescriptor.WithSchemaBinding

    Public Overridable Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBView(Me) With {.Parent = parent}
    End Function

    Public Sub New()

    End Sub

    Public Sub New(descriptor As IDBObjectDescriptor)
        MyClass.New()

        With CType(descriptor, IDBViewDescriptor)
            WithSchemaBinding = .WithSchemaBinding
            Body = .Body
        End With
    End Sub


End Class

