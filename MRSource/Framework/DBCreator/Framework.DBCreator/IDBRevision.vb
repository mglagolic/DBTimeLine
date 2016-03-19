Public Interface IDBRevision
    Property DBRevisionType As eDBRevisionType
    Property Parent As IDBObject
    Property Created As Date
    Property Granulation As Integer
    Property DBObject As IDBObject
End Interface