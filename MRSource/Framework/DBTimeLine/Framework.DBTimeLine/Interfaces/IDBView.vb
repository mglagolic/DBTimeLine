Public Interface IDBView
    Inherits IDBObject

    Function AddIndex(descriptor As IDBIndexDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBIndex
    Sub AddClaims(created As Date)
End Interface
