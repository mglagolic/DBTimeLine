Public Interface IDBObject
    Inherits IDBChained
    Inherits IDBParent

    Property Name As String
    Property Descriptor As IDBObjectDescriptor

    ReadOnly Property DBCreator As DBCreator

    ReadOnly Property ModuleKey() As String
    ReadOnly Property SchemaName As String
    ReadOnly Property SchemaObjectName() As String

    ReadOnly Property ObjectType As eDBObjectType
    Function AddRevision(revision As IDBRevision, Optional descriptor As IDBObjectDescriptor = Nothing) As IDBRevision
    ReadOnly Property Revisions As List(Of IDBRevision)
    Function GetFullName() As String

    'Function FindRevision(created As Date, granulation As Integer) As IDBRevision

    Function GetSqlCreate(sqlGenerator As IDBSqlGenerator) As String
    Function GetSqlModify(sqlGenerator As IDBSqlGenerator) As String
    Function GetSqlDelete(sqlGenerator As IDBSqlGenerator) As String
End Interface
