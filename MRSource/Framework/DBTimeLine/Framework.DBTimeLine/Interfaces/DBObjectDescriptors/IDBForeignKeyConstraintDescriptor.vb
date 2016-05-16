Public Interface IDBForeignKeyConstraintDescriptor
    Inherits IDBConstraintDescriptor

    ReadOnly Property FKTableColumns As List(Of String)
    ReadOnly Property PKTableColumns As List(Of String)
    Property PKTableSchemaName As String
    Property PKTableName As String

End Interface