Public Class DBSchema
    Inherits DBObject
    Implements IDBSchema

    Public Sub New()

    End Sub

    Public Sub New(descriptor As DBSchemaDescriptor)
        MyClass.New

        ApplyDescriptor(descriptor)
    End Sub

    Public Overrides ReadOnly Property DBObjectType As eDBObjectType
        Get
            Return eDBObjectType.Schema
        End Get
    End Property

    Public Overrides Sub ApplyDescriptor(descriptor As IDBObjectDescriptor)

    End Sub

    Public Overrides Function GetDescriptor() As IDBObjectDescriptor
        Return New DBSchemaDescriptor
    End Function

    Public Function AddTable(tableName As String, descriptor As IDBTableDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBTable Implements IDBSchema.AddTable
        If Not DBObjects.ContainsKey(tableName) Then
            Dim newDBObject As IDBObject = descriptor.GetDBObjectInstance
            With newDBObject
                .Name = tableName
                .Parent = Me
            End With

            DBObjects.Add(tableName, newDBObject)
            DBCreator.DBTables.Add(tableName, newDBObject)
        End If
        Dim table As DBTable = DBObjects(tableName)

        If createRevision IsNot Nothing Then
            table.AddRevision(createRevision)
        End If

        Return table
    End Function

    Public Overrides Function GetSqlCreate() As String Implements IDBObject.GetSqlCreate
        Dim ret As String = ""

        ret = <string>GO
CREATE SCHEMA <%= Name %>
GO
</string>.Value

        Return ret
    End Function

    Public Overrides Function GetSqlModify() As String Implements IDBObject.GetSqlModify
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetSqlDelete() As String Implements IDBObject.GetSqlDelete
        Dim ret As String = ""

        ret =
<string>GO
DROP SCHEMA <%= Name & vbNewLine %>
GO
</string>.Value

        Return ret
    End Function
End Class
