Imports MRFramework.MRDBCreator

Public Interface IDBTable
    Property CreatorFieldName As String
    Property CreatorFieldDescriptor As DBFieldDescriptor
End Interface


Public Class DBTableDescriptor
    Implements IDBObjectDescriptor
    Implements IDBTable

    Public Property CreatorFieldDescriptor As DBFieldDescriptor Implements IDBTable.CreatorFieldDescriptor
    Public Property CreatorFieldName As String Implements IDBTable.CreatorFieldName
End Class

Public Class DBTable
    Inherits DBObject
    Implements IDBTable

    Public Sub New()

    End Sub

    Public Sub New(descriptor As DBTableDescriptor)
        MyClass.New

        ApplyDescriptor(descriptor)
    End Sub

    Public Overrides ReadOnly Property DBObjectType As eDBObjectType
        Get
            Return eDBObjectType.Table
        End Get
    End Property

    Public Property CreatorFieldDescriptor As DBFieldDescriptor Implements IDBTable.CreatorFieldDescriptor
    Public Property CreatorFieldName As String Implements IDBTable.CreatorFieldName

    Private ReadOnly _DBFields As New Dictionary(Of String, DBField)
    Public ReadOnly Property DBFields As Dictionary(Of String, DBField)
        Get
            Return _DBFields
        End Get
    End Property

    Public Function AddField(fieldName As String, descriptor As DBFieldDescriptor, Optional createRevision As DBRevision = Nothing) As DBField
        If Not DBFields.ContainsKey(fieldName) Then
            Dim newField As New DBField(descriptor) With {.Name = fieldName, .Parent = Me}
            DBFields.Add(fieldName, newField)
            Creator.DBFields.Add(fieldName, newField)
        End If

        Dim field As DBField = DBFields(fieldName)

        If createRevision IsNot Nothing Then
            field.AddRevision(createRevision)
        End If

        Return field
    End Function

    Public Overrides Sub ApplyDescriptor(descriptor As IDBObjectDescriptor)
        With DirectCast(descriptor, DBTableDescriptor)
            CreatorFieldDescriptor = .CreatorFieldDescriptor
            CreatorFieldName = .CreatorFieldName
            DBFields.Add(CreatorFieldName, New DBField(CreatorFieldDescriptor) With {.Parent = Me, .Name = CreatorFieldName})
        End With
    End Sub

    Public Overrides Function GetDescriptor() As IDBObjectDescriptor
        Return New DBTableDescriptor() With {.CreatorFieldDescriptor = CreatorFieldDescriptor, .CreatorFieldName = CreatorFieldName}
    End Function

    Public Overrides Function GetSqlCreate() As String
        Dim ret As String =
<string>;CREATE TABLE <%= SchemaName %>.<%= Name %> 
(
    <%= CreatorFieldName & " " %><%= CreatorFieldDescriptor.GetFieldTypeSql %> 
)
</string>.Value

        Return ret
    End Function

    Public Overrides Function GetSqlModify() As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetSqlDelete() As String
        Dim ret As String =
<string>;DROP TABLE <%= SchemaName %>.<%= Name %>
</string>.Value

        Return ret
    End Function
End Class
