Public Class DBSchema
    Inherits DBObject

    Implements IDBSchema

    Public Sub New()

    End Sub

    Public Overrides ReadOnly Property ObjectTypeOrdinal As Integer
        Get
            Return 0
        End Get
    End Property

    Public Overrides ReadOnly Property ObjectTypeName As String
        Get
            Return "Schema"
        End Get
    End Property

    Public Function AddTable(tableName As String, descriptor As IDBTableDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBTable Implements IDBSchema.AddTable
        Return MyBase.AddDBObject(tableName, descriptor, createRevision)
    End Function

    Public Function AddView(viewName As String, descriptor As IDBViewDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBView Implements IDBSchema.AddView
        Return MyBase.AddDBObject(viewName, descriptor, createRevision)
    End Function

    Public Function AddExecuteOnceTask(created As Date, granulation As Integer, sql As String) As IDBRevision Implements IDBSchema.AddExecuteOnceTask
        Dim returnSql As RevisionTaskDelegate = Function(sender, DBType) sql

        Return AddRevision(New DBRevision(New DateTime(2016, 9, 21), 0, eDBRevisionType.Task, returnSql))
    End Function

    Public Overrides Function GetSqlCreate(dBType As eDBType) As String
        Dim ret As String = ""
        ret = String.Format("GO
CREATE SCHEMA {0}
GO
", Name)

        Return ret
    End Function

    Public Overrides Function GetSqlModify(dBType As eDBType) As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetSqlDelete(dBType As eDBType) As String
        Dim ret As String = ""
        ret = String.Format("GO
DROP SCHEMA {0}
GO
", Name)

        Return ret
    End Function


End Class
