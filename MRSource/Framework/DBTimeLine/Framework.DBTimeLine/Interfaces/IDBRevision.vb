Public Delegate Function RevisionTaskDelegate(sender As IDBRevision, dBType As eDBType) As String

Public Interface IDBRevision
    Property DBRevisionType As eDBRevisionType
    Property Parent As IDBObject
    Property Created As Date
    Property Granulation As Integer
    Property PreSqlTask As RevisionTaskDelegate
    Property PostSqlTask As RevisionTaskDelegate
    Function GetSql() As String
End Interface