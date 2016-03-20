Public Delegate Function RevisionTaskDelegate(sender As IDBRevision) As String

Public Interface IDBRevision
    Property DBRevisionType As eDBRevisionType
    Property Parent As IDBObject
    Property Created As Date
    Property Granulation As Integer
    Property PreSqlTask As RevisionTaskDelegate
    Property PostSqlTask As RevisionTaskDelegate
    Function GetSql() As String
End Interface