Public Interface IDBFieldCommon
    Property FieldType As eFieldType
    Property Size As Integer
    Property Precision As Integer
    Property IsIdentity As Boolean
    Property Nullable As Boolean
    Property DefaultValue As String
End Interface

Public Interface IDBField
    Inherits IDBFieldCommon
    Inherits IDBObject

End Interface