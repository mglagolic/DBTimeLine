﻿Public Interface IDBTable
    Inherits IDBObject

    Function AddField(fieldName As String, descriptor As IDBFieldDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBObject
    Function AddConstraint(descriptor As IDBConstraintDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBObject

End Interface