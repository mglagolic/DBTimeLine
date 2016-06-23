Public MustInherit Class DBFieldType
    Implements IDBFieldType

    Public MustOverride Function GetFieldTypeSql(fieldDescriptor As IDBFieldDescriptor, dBType As eDBType) As String Implements IDBFieldType.GetFieldTypeSql

End Class

'Public Enum eDBFieldType
'    Guid = 0
'    Nvarchar = 1
'    [Decimal] = 2
'    Datetime = 3
'    [Integer] = 4
'    [Boolean] = 5
'End Enum
