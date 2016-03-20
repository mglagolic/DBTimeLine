Public Class DBTable
    Inherits DBObject
    Implements IDBTable

    Public Sub New()

    End Sub

    Public Sub New(descriptor As IDBTableDescriptor)
        MyClass.New

        Me.Descriptor = descriptor
        If Not String.IsNullOrWhiteSpace(descriptor.CreatorFieldName) AndAlso descriptor.CreatorFieldDescriptor IsNot Nothing Then
            AddField(descriptor.CreatorFieldName, descriptor.CreatorFieldDescriptor)
        End If
    End Sub

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.Table
        End Get
    End Property

    Public Function AddField(fieldName As String, descriptor As IDBFieldDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBObject Implements IDBTable.AddField
        If Not DBObjects.ContainsKey(fieldName) Then
            Dim newDBObject As IDBObject = descriptor.GetDBObjectInstance(Me)
            With newDBObject
                .Name = fieldName
            End With

            DBObjects.Add(fieldName, newDBObject)
        End If

        Dim field As DBField = DBObjects(fieldName)

        If createRevision IsNot Nothing Then
            field.AddRevision(createRevision)
        End If

        Return field
    End Function

    Public Overrides Function GetSqlCreate() As String Implements IDBObject.GetSqlCreate
        Dim ret As String = ""
        With CType(Descriptor, IDBTableDescriptor)
            ret =
<string>
CREATE TABLE <%= SchemaName %>.<%= Name %> 
(
    <%= .CreatorFieldName & " " %><%= .CreatorFieldDescriptor.GetFieldTypeSql %>
)
</string>.Value
        End With

        Return ret
    End Function

    Public Overrides Function GetSqlDelete() As String Implements IDBObject.GetSqlDelete
        Dim ret As String = ""

        ret =
<string>
DROP TABLE <%= SchemaName %>.<%= Name %>
</string>.Value


        Return ret
    End Function

    Public Overrides Function GetSqlModify() As String Implements IDBObject.GetSqlModify
        Throw New NotImplementedException()
    End Function

End Class
