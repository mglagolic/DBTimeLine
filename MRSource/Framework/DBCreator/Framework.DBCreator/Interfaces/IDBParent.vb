Public Interface IDBParent
    ReadOnly Property DBObjects As Dictionary(Of String, IDBObject)
    Function AddDBObject(objectName As String, descriptor As IDBObjectDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBObject

End Interface
