Imports MRFramework.MRDBCreator

Public Interface IDBField
    Property FieldType As eFieldType
    Property Size As Integer
    Property Precision As Integer
    Property IsIdentity As Boolean
    Property Nullable As Boolean
    Property DefaultValue As String
End Interface

Public Enum eFieldType
    Guid = 0
    Nvarchar = 1
    [Decimal] = 2
    Datetime = 3
    [Integer] = 4
End Enum

Public Class DBFieldDescriptor
    Implements IDBObjectDescriptor
    Implements IDBField

    Public Property FieldType As eFieldType Implements IDBField.FieldType
    Public Property Size As Integer = 0 Implements IDBField.Size
    Public Property Precision As Integer = 0 Implements IDBField.Precision
    Public Property IsIdentity As Boolean = False Implements IDBField.IsIdentity
    Public Property Nullable As Boolean Implements IDBField.Nullable
    Public Property DefaultValue As String Implements IDBField.DefaultValue

    Public ReadOnly Property FieldTypeName As String
        Get
            Return [Enum].GetName(GetType(eFieldType), FieldType)
        End Get
    End Property

    Public Sub New()

    End Sub

    Public Sub New(descriptor As DBFieldDescriptor)
        MyClass.New()

        With descriptor
            FieldType = .FieldType
            Size = .Size
            Precision = .Precision
            IsIdentity = .IsIdentity
            Nullable = .Nullable
            DefaultValue = .DefaultValue
        End With
    End Sub

End Class

Public Class DBField
    Inherits DBObject
    Implements IDBField

    Public Property FieldType As eFieldType Implements IDBField.FieldType
    Public Property Size As Integer = 0 Implements IDBField.Size
    Public Property Precision As Integer = 0 Implements IDBField.Precision
    Public Property IsIdentity As Boolean = False Implements IDBField.IsIdentity
    Public Property Nullable As Boolean = True Implements IDBField.Nullable
    Public Property DefaultValue As String Implements IDBField.DefaultValue

    Public Sub New()

    End Sub

    Public Sub New(descriptor As DBFieldDescriptor)
        MyClass.New()

        ApplyDescriptor(descriptor)
    End Sub

    Public Overrides Sub ApplyDescriptor(descriptor As IDBObjectDescriptor)
        With CType(descriptor, DBFieldDescriptor)
            FieldType = .FieldType
            Size = .Size
            Precision = .Precision
            IsIdentity = .IsIdentity
            Nullable = .Nullable
            DefaultValue = .DefaultValue
        End With
    End Sub

    Public Overrides Function GetDescriptor() As IDBObjectDescriptor
        Dim ret As New DBFieldDescriptor With {.FieldType = FieldType, .Precision = Precision, .Size = Size, .DefaultValue = DefaultValue, .IsIdentity = IsIdentity, .Nullable = Nullable}

        Return ret
    End Function

    Public Overrides Function GetSqlCreate() As String
        Throw New NotImplementedException()
    End Function

    Public Overrides ReadOnly Property DBObjectType As eDBObjectType
        Get
            Return eDBObjectType.Field
        End Get
    End Property

End Class

