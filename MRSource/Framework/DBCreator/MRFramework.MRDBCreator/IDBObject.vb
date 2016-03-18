Public Enum eDBObjectType
    Schema = 0
    Table = 1
    Field = 2
    View = 3
End Enum

Public Interface IDBObject
    Inherits IDBChained

    Property Name As String
    ReadOnly Property DBCreator As DBCreator
    ReadOnly Property SchemaName As String
    ReadOnly Property DBObjectType As eDBObjectType
    Function AddRevision(revision As DBRevision, Optional descriptor As IDBObjectDescriptor = Nothing) As DBRevision
    Function GetDescriptor() As IDBObjectDescriptor
    ReadOnly Property Revisions As List(Of DBRevision)
    Function GetFullName() As String

    Function GetSqlCreate() As String
    Function GetSqlModify() As String
    Function GetSqlDelete() As String
End Interface
