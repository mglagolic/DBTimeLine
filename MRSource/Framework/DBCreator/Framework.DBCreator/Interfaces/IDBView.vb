Public Interface IDBView
    Inherits IDBObject

    Function AddIndex(descriptor As IDBIndexDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBIndex
End Interface
