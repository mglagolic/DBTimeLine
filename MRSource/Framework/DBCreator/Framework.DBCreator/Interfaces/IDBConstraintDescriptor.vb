Public Interface IDBConstraintDescriptor
    Inherits IDBObjectDescriptor

    Property ConstraintName As String
    Function GetConstraintName(schemaName As String, tableName As String) As String


End Interface
