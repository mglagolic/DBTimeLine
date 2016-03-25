Public Interface IDBTableDescriptor
    Inherits IDBObjectDescriptor

    Property CreatorFieldName As String
    Property CreatorFieldDescriptor As IDBFieldDescriptor

End Interface

Public Interface IDBTable
    Inherits IDBObject

    Function AddField(fieldName As String, descriptor As IDBFieldDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBObject
    Function AddConstraint(descriptor As IDBObjectDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBObject

End Interface