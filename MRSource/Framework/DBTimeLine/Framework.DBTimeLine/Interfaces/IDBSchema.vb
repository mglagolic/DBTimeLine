Public Interface IDBSchema
    Inherits IDBObject

    Function AddTable(tableName As String, descriptor As IDBTableDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBTable
    Function AddView(viewName As String, descriptor As IDBViewDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBView
End Interface

