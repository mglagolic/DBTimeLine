Public Enum eFieldType
    Guid = 0
    Nvarchar = 1
    [Decimal] = 2
    Datetime = 3
    [Integer] = 4
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

    Public Overrides ReadOnly Property DBObjectType As eDBObjectType
        Get
            Return eDBObjectType.Field
        End Get
    End Property
End Class
