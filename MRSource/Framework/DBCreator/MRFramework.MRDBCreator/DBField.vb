Public Enum eFieldType
    guid = 0
    nvarchar = 1
    [decimal] = 2
End Enum
Public Class DBField
    Inherits DBObject

    Public Sub New()

    End Sub

    Public Sub New(name As String, fieldType As eFieldType)
        MyClass.New()

        Me.Name = name
        Me.FieldType = fieldType
    End Sub

    Public Sub New(name As String, fieldType As eFieldType, size As Integer, precision As Integer)
        MyClass.New(name, fieldType)
        Me.Size = size
        Me.Precision = precision
    End Sub

    Public Property FieldType As eFieldType
    Public Property Size As Integer
    Public Property Precision As Integer

    Public Overrides ReadOnly Property DBObjectType As eDBObjectTypes
        Get
            Return eDBObjectTypes.Field
        End Get
    End Property
End Class
