Public Enum eDBObjectTypes
    Schema = 0
    Table = 1
    Field = 2
    View = 3
End Enum

Public Interface IDBObject
    Property Name As String
    ReadOnly Property DBObjectType As eDBObjectTypes
    Property Parent As IDBObject
    Function AddRevision(revision As DBRevision) As DBRevision
    ReadOnly Property Revisions As List(Of DBRevision)
End Interface
