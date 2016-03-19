Imports Framework.DBCreator

Public Class DBFieldDescriptor
    Implements IDBFieldDescriptor

    Public Property FieldType As eFieldType Implements IDBFieldDescriptor.FieldType
    Public Property Size As Integer = 0 Implements IDBFieldDescriptor.Size
    Public Property Precision As Integer = 0 Implements IDBFieldDescriptor.Precision
    Public Property IsIdentity As Boolean = False Implements IDBFieldDescriptor.IsIdentity
    Public Property Nullable As Boolean Implements IDBFieldDescriptor.Nullable
    Public Property DefaultValue As String Implements IDBFieldDescriptor.DefaultValue

    Public Overridable Function GetDBObjectInstance() As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBField(Me)
    End Function

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

    Public Function GetFieldTypeSql() As String Implements IDBFieldDescriptor.GetFieldTypeSql
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

    '    Public Overridable Function GetSqlCreate(dBObject As IDBObject) As String Implements IDBObjectDescriptor.GetSqlCreate
    '        Dim ret As String = ""
    '        With DirectCast(dBObject, DBField)
    '            ret =
    '<string>ALTER TABLE <%= .SchemaName %>.<%= DirectCast(.Parent, IDBObject).Name %> ADD
    '        	<%= .Name & " " %><%= GetFieldTypeSql() & vbNewLine %></string>.Value
    '        End With

    '        Return ret
    '    End Function

    '    Public Overridable Function GetSqlModify(dBObject As IDBObject) As String Implements IDBObjectDescriptor.GetSqlModify
    '        Dim ret As String = ""
    '        With DirectCast(dBObject, DBField)
    '            ret =
    '<string>ALTER TABLE <%= .SchemaName %>.<%= DirectCast(.Parent, IDBObject).Name %> ALTER COLUMN
    '        	<%= .Name & " " %><%= GetFieldTypeSql() & vbNewLine %></string>.Value
    '        End With
    '        Return ret
    '    End Function

    '    Public Overridable Function GetSqlDelete(dBObject As IDBObject) As String Implements IDBObjectDescriptor.GetSqlDelete
    '        Dim ret As String = ""
    '        With DirectCast(dBObject, DBField)
    '            ret =
    '<string>ALTER TABLE <%= .SchemaName %>.<%= DirectCast(.Parent, IDBObject).Name %> DROP COLUMN
    '        	<%= .Name & " " & vbNewLine %></string>.Value
    '        End With
    '        Return ret
    '    End Function

End Class