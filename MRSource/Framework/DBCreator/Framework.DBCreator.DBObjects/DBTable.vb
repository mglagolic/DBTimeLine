Public Class DBTable
    Inherits DBObject
    Implements IDBTableCommon
    Implements IDBTable


    Public Sub New()

    End Sub

    Public Sub New(descriptor As IDBTableDescriptor)
        MyClass.New

        ApplyDescriptor(descriptor)
    End Sub

    Public Overrides ReadOnly Property DBObjectType As eDBObjectType
        Get
            Return eDBObjectType.Table
        End Get
    End Property

    Public Property CreatorFieldDescriptor As IDBFieldDescriptor Implements IDBTableCommon.CreatorFieldDescriptor
    Public Property CreatorFieldName As String Implements IDBTableCommon.CreatorFieldName

    Public Function AddField(fieldName As String, descriptor As IDBFieldDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBObject Implements IDBTable.AddField
        If Not DBObjects.ContainsKey(fieldName) Then
            Dim newDBObject As IDBObject = descriptor.GetDBObjectInstance
            With newDBObject
                .Name = fieldName
                .Parent = Me
            End With

            DBObjects.Add(fieldName, newDBObject)
            DBCreator.DBFields.Add(fieldName, newDBObject)
        End If

        Dim field As DBField = DBObjects(fieldName)

        If createRevision IsNot Nothing Then
            field.AddRevision(createRevision)
        End If

        Return field
    End Function

    Public Overrides Sub ApplyDescriptor(descriptor As IDBObjectDescriptor)
        With DirectCast(descriptor, DBTableDescriptor)
            CreatorFieldDescriptor = .CreatorFieldDescriptor
            CreatorFieldName = .CreatorFieldName

            Dim fld As IDBObject = CreatorFieldDescriptor.GetDBObjectInstance
            With fld
                .Parent = Me
                .Name = CreatorFieldName
            End With

            DBObjects.Add(CreatorFieldName, fld)
        End With
    End Sub

    Public Overrides Function GetDescriptor() As IDBObjectDescriptor
        Return New DBTableDescriptor() With {.CreatorFieldDescriptor = CreatorFieldDescriptor, .CreatorFieldName = CreatorFieldName}
    End Function

    Public Overrides Function GetSqlCreate() As String Implements IDBObject.GetSqlCreate
        Dim ret As String = ""

        ret =
<string>
CREATE TABLE <%= SchemaName %>.<%= Name %> 
(
    <%= CreatorFieldName & " " %><%= CreatorFieldDescriptor.GetFieldTypeSql %>
)
</string>.Value


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
