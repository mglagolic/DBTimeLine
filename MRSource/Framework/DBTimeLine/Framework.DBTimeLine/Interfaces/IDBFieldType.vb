Public Interface IDBFieldType

    Function GetFieldTypeSql(fieldDescriptor As IDBFieldDescriptor, dBType As eDBType) As String

End Interface
