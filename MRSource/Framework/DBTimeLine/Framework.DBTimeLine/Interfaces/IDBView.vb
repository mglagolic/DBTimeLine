Public Interface IDBView
    Inherits IDBRecreatableObject

    Function AddIndex(descriptor As IDBIndexDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBIndex
End Interface
