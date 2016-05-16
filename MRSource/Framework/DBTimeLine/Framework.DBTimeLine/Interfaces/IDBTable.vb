Public Interface IDBTable
    Inherits IDBObject

    Function AddField(fieldName As String, descriptor As IDBFieldDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBField
    Function AddConstraint(descriptor As IDBConstraintDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBConstraint
    Function AddIndex(descriptor As IDBIndexDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBIndex

End Interface