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

    Public Overrides Function GetSqlCreate() As String Implements IDBObject.GetSqlCreate
        Dim ret As String = ""

        ret =
<string>ALTER TABLE <%= SchemaName %>.<%= DirectCast(Parent, IDBObject).Name %> ADD
        	<%= Name & " " %><%= DirectCast(GetDescriptor(), IDBFieldDescriptor).GetFieldTypeSql() & vbNewLine %></string>.Value


        Return ret
    End Function

    Public Overrides Function GetSqlModify() As String Implements IDBObject.GetSqlModify
        Dim ret As String = ""

        ret =
<string>ALTER TABLE <%= SchemaName %>.<%= DirectCast(Parent, IDBObject).Name %> ALTER COLUMN
        	<%= Name & " " %><%= DirectCast(GetDescriptor(), IDBFieldDescriptor).GetFieldTypeSql() & vbNewLine %></string>.Value

        Return ret
    End Function

    Public Overrides Function GetSqlDelete() As String Implements IDBObject.GetSqlDelete
        Dim ret As String = ""

        ret =
<string>ALTER TABLE <%= SchemaName %>.<%= DirectCast(Parent, IDBObject).Name %> DROP COLUMN
        	<%= Name & " " & vbNewLine %></string>.Value

        Return ret
    End Function


End Class

