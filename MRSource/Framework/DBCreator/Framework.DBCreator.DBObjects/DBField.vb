Public Class DBField
    Inherits DBObject
    Implements IDBField

    Public Sub New()

    End Sub

    Public Sub New(descriptor As DBFieldDescriptor)
        MyClass.New()

        Me.Descriptor = descriptor
    End Sub

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.Field
        End Get
    End Property

    Public Overrides Function GetSqlCreate() As String Implements IDBObject.GetSqlCreate
        Dim ret As String = ""

        ret =
<string>ALTER TABLE <%= SchemaName %>.<%= DirectCast(Parent, IDBObject).Name %> ADD
        	<%= Name & " " %><%= CType(Descriptor, IDBFieldDescriptor).GetFieldTypeSql() & vbNewLine %></string>.Value

        Return ret
    End Function

    Public Overrides Function GetSqlModify() As String Implements IDBObject.GetSqlModify
        Dim ret As String = ""

        ret =
<string>ALTER TABLE <%= SchemaName %>.<%= DirectCast(Parent, IDBObject).Name %> ALTER COLUMN
        	<%= Name & " " %><%= CType(Descriptor, IDBFieldDescriptor).GetFieldTypeSql() & vbNewLine %></string>.Value

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

