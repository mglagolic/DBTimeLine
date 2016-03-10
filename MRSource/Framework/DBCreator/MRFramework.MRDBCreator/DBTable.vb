Public Class DBTable
    Inherits DBObject

    Public Sub New()

    End Sub

    Public Sub New(name As String)
        MyClass.New()

        Me.Name = name
    End Sub

    Public Overrides ReadOnly Property DBObjectType As eDBObjectTypes
        Get
            Return eDBObjectTypes.Table
        End Get
    End Property

    Private ReadOnly _DBFields As New List(Of DBField)
    Public ReadOnly Property DBFields As List(Of DBField)
        Get
            Return _DBFields
        End Get
    End Property

    Public Function AddField(field As DBField, createRevision As DBRevision) As DBField
        Me.DBFields.Add(field)
        field.Parent = Me
        field.AddRevision(createRevision)

        Return field
    End Function
End Class
