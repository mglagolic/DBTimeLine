Public Enum eDBObjectType
    Schema = 0
    Table = 1
    Field = 2
    View = 3
End Enum

Public Interface IDBObject
    Property Name As String
    ReadOnly Property DBObjectType As eDBObjectType
    Property Parent As IDBObject
    Function AddRevision(revision As DBRevision, Optional dbObject As IDBObject = Nothing) As DBRevision
    ReadOnly Property Revisions As List(Of DBRevision)
    Function GetFullName() As String
End Interface
