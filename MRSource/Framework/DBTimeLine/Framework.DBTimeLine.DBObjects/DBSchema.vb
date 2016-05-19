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
