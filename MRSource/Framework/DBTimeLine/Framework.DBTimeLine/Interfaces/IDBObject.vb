Public Interface IDBObject
    Inherits IDBChained
    Inherits IDBParent

    Property Name As String
    Property Descriptor As IDBObjectDescriptor

    ReadOnly Property DBTimeLiner As DBTimeLiner

    ReadOnly Property ModuleKey() As String
    ReadOnly Property SchemaName As String
    ReadOnly Property SchemaObjectName() As String

    ReadOnly Property ObjectTypeOrdinal As Integer
    ReadOnly Property ObjectTypeName As String

    'ReadOnly Property ObjectType As eDBObjectType
    Function AddRevision(revision As IDBRevision, Optional descriptor As IDBObjectDescriptor = Nothing) As IDBRevision
    ReadOnly Property Revisions As List(Of IDBRevision)
    Function GetFullName() As String

    Function GetSqlCreate(dBType As eDBType) As String
    Function GetSqlModify(dBType As eDBType) As String
    Function GetSqlDelete(dBType As eDBType) As String

End Interface
