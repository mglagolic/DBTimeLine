Public Class DBFieldDescriptor
    Implements IDBFieldDescriptor

    Public Property FieldType As eDBFieldType Implements IDBFieldDescriptor.FieldType
    Public Property Size As Integer = 0 Implements IDBFieldDescriptor.Size
    Public Property Precision As Integer = 0 Implements IDBFieldDescriptor.Precision
    Public Property IsIdentity As Boolean = False Implements IDBFieldDescriptor.IsIdentity
    Public Property Nullable As Boolean = True Implements IDBFieldDescriptor.Nullable
    Public Property DefaultValue As String Implements IDBFieldDescriptor.DefaultValue

    Public Overridable Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBField() With {.Parent = parent, .Descriptor = Me}
    End Function

    Public Sub New()

    End Sub

    Public Sub New(descriptor As IDBObjectDescriptor)
        MyClass.New()

        With CType(descriptor, IDBFieldDescriptor)
            FieldType = .FieldType
            Size = .Size
            Precision = .Precision
            IsIdentity = .IsIdentity
            Nullable = .Nullable
            DefaultValue = .DefaultValue
        End With
    End Sub



End Class