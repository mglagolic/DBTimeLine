

Public Interface IDBFieldDescriptor
    Inherits IDBObjectDescriptor

    Property FieldType As eFieldType
    Property Size As Integer
    Property Precision As Integer
    Property IsIdentity As Boolean
    Property Nullable As Boolean
    Property DefaultValue As String

    Function GetFieldTypeSql() As String
End Interface


Public Interface IDBField
    Inherits IDBObject

End Interface