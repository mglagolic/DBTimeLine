Public Interface IDBIndexDescriptor
    Inherits IDBObjectDescriptor

    Property IndexName As String
    Property Unique As Boolean
    Property Clustered As Boolean

    ReadOnly Property Keys As List(Of String)
    ReadOnly Property Include As List(Of String)

    Function GetIndexName(schemaName As String, tableName As String) As String

End Interface