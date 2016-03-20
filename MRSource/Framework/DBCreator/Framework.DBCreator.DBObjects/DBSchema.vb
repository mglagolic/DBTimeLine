Public Class DBSchema
    Inherits DBObject
    Implements IDBSchema

    Public Sub New()

    End Sub

    Public Sub New(descriptor As DBSchemaDescriptor)
        MyClass.New

        Me.Descriptor = descriptor
    End Sub

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.Schema
        End Get
    End Property

    Public Function AddTable(tableName As String, descriptor As IDBTableDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBTable Implements IDBSchema.AddTable
        If Not DBObjects.ContainsKey(tableName) Then
            Dim newDBObject As IDBObject = descriptor.GetDBObjectInstance(Me)
            With newDBObject
                .Name = tableName
            End With
            ' TODO - dodati fullobject name u idbobject tako da dic ne bi pucao ako postoje npr dvije scheme s istom tablicom
            DBObjects.Add(tableName, newDBObject)
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
