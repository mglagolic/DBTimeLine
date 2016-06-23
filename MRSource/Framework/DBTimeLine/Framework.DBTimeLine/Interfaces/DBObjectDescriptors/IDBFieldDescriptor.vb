Public Interface IDBFieldDescriptor
    Inherits IDBObjectDescriptor

    Property FieldType As IDBFieldType
    Property Size As Integer
    Property Precision As Integer
    Property IsIdentity As Boolean
    Property Nullable As Boolean
    Property DefaultValue As String

End Interface