Public Interface IDBFieldGenerator
    Inherits IDBObjectGenerator

    Function GetFieldTypeSql(descriptor As IDBFieldDescriptor) As String

End Interface