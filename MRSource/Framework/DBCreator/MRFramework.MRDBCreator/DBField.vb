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

    Public Function GetFieldTypeSql() As String
        Dim ret As String = ""

        Select Case FieldType
            Case eFieldType.Guid
                ret = "UNIQUEIDENTIFIER"
            Case eFieldType.Integer
                ret = "INTEGER"
            Case eFieldType.Nvarchar
                ret = "NVARCHAR(" & CStr(IIf(Size = -1, "MAX", Size.ToString)) & ")"
            Case eFieldType.Datetime
                ret = "DATETIME"
            Case eFieldType.Decimal
                ret = "DECIMAL(" & Size.ToString & ", " & Precision.ToString & ")"
            Case Else
                Throw New NotSupportedException("Unsupported eFieldType.")
        End Select

        If Nullable Then
            ret &= " NULL"
        Else
            ret &= " NOT NULL"
        End If

        Return ret
    End Function

    Public Overridable Function GetSqlCreate(dBObject As IDBObject) As String Implements IDBObjectDescriptor.GetSqlCreate
        Dim ret As String = ""
        With DirectCast(dBObject, DBField)
            ret =
<string>;ALTER TABLE <%= .SchemaName %>.<%= DirectCast(.Parent, IDBObject).Name %> ADD
        	<%= .Name & " " %><%= GetFieldTypeSql() %>
</string>.Value
        End With

        Return ret
    End Function

    Public Overridable Function GetSqlModify(dBObject As IDBObject) As String Implements IDBObjectDescriptor.GetSqlModify
        Dim ret As String = ""
        With DirectCast(dBObject, DBField)
            ret =
<string>;ALTER TABLE <%= .SchemaName %>.<%= DirectCast(.Parent, IDBObject).Name %> ALTER COLUMN
        	<%= .Name & " " %><%= GetFieldTypeSql() & vbNewLine %></string>.Value
        End With
        Return ret
    End Function

    Public Overridable Function GetSqlDelete(dBObject As IDBObject) As String Implements IDBObjectDescriptor.GetSqlDelete
        Dim ret As String = ""
        With DirectCast(dBObject, DBField)
            ret =
<string>;ALTER TABLE <%= .SchemaName %>.<%= DirectCast(.Parent, IDBObject).Name %> DROP COLUMN
        	<%= .Name & " " %>
</string>.Value
        End With
        Return ret
    End Function


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
        Dim ret As New DBFieldDescriptor With {
            .FieldType = FieldType,
            .Precision = Precision,
            .Size = Size,
            .DefaultValue = DefaultValue,
            .IsIdentity = IsIdentity,
            .Nullable = Nullable
        }

        Return ret
    End Function

    Public Overrides ReadOnly Property DBObjectType As eDBObjectType
        Get
            Return eDBObjectType.Field
        End Get
    End Property


End Class

