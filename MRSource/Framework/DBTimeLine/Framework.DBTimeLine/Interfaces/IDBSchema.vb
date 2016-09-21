Public Interface IDBSchema
    Inherits IDBObject

    Function AddTable(tableName As String, descriptor As IDBTableDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBTable
    Function AddView(viewName As String, descriptor As IDBViewDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBView
    Function AddExecuteOnceTask(created As Date, granulation As Integer, sql As String) As IDBRevision
End Interface

