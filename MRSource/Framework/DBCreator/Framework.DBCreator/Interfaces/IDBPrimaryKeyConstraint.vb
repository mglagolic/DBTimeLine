Public Interface IDBPrimaryKeyConstraintDescriptor
    Inherits IDBObjectDescriptor

    Property ConstraintName As String
    ReadOnly Property Columns As List(Of String)

End Interface

Public Interface IDBPrimaryKeyConstraint
    Inherits IDBObject

End Interface