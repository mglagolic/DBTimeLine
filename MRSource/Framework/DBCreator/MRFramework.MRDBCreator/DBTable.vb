Public Interface IDBTable

End Interface


Public Class DBTableDescriptor
    Implements IDBObjectDescriptor
    Implements IDBTable

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

    Private ReadOnly _DBFields As New Dictionary(Of String, DBField)
    Public ReadOnly Property DBFields As Dictionary(Of String, DBField)
        Get
            Return _DBFields
        End Get
    End Property

    Public Function AddField(fieldName As String, descriptor As DBFieldDescriptor, Optional createRevision As DBRevision = Nothing) As DBField
        If Not DBFields.ContainsKey(fieldName) Then
            DBFields.Add(fieldName, New DBField(descriptor) With {.Name = fieldName, .Parent = Me})
        End If
        Dim field As DBField = DBFields(fieldName)

        If createRevision IsNot Nothing Then
            field.AddRevision(createRevision)
        End If

        Return field
    End Function

    Public Overrides Sub ApplyDescriptor(descriptor As IDBObjectDescriptor)

    End Sub

    Public Overrides Function GetDescriptor() As IDBObjectDescriptor
        Return New DBTableDescriptor
    End Function
End Class
