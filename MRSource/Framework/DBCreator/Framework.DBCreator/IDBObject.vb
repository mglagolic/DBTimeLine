Public Interface IDBObject
    Inherits IDBChained

    Property Name As String
    Property Descriptor As IDBObjectDescriptor

    ReadOnly Property DBCreator As DBCreator
    ReadOnly Property SchemaName As String
    ReadOnly Property DBObjectType As eDBObjectType
    Function AddRevision(revision As IDBRevision, Optional descriptor As IDBObjectDescriptor = Nothing) As IDBRevision
    ReadOnly Property Revisions As List(Of IDBRevision)
    Function GetFullName() As String
    Function FindRevision(created As Date, granulation As Integer) As IDBRevision
    ReadOnly Property DBObjects As Dictionary(Of String, IDBObject)

    Function GetSqlCreate() As String
    Function GetSqlModify() As String
    Function GetSqlDelete() As String
End Interface
