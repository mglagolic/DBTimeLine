Public Class DBSchemaGenerator
    Implements IDBObjectGenerator

    Public Function GetSqlCreate(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlCreate
        Dim ret As String = ""
        ret = String.Format("GO
CREATE SCHEMA {0}
GO
", dbObject.Name)

        Return ret
    End Function

    Public Function GetSqlDelete(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlDelete
        Dim ret As String = ""
        ret = String.Format("GO
DROP SCHEMA {0}
GO
", dbObject.Name)

        Return ret
    End Function

    Public Function GetSqlModify(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlModify
        Throw New NotImplementedException
    End Function
End Class
