Public Class DBSchemaDescriptor
    Implements IDBSchemaDescriptor

    Public Overridable Function GetDBObjectInstance() As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBSchema(Me)
    End Function

    '    Public Overridable Function GetSqlCreate(dBObject As IDBObject) As String Implements IDBObjectDescriptor.GetSqlCreate
    '        Dim ret As String = ""
    '        With DirectCast(dBObject, DBSchema)
    '            ret = <string>GO
    'CREATE SCHEMA <%= .Name %>
    'GO
    '</string>.Value
    '        End With

    '        Return ret
    '    End Function

    '    Public Overridable Function GetSqlModify(dBObject As IDBObject) As String Implements IDBObjectDescriptor.GetSqlModify
    '        Throw New NotImplementedException()
    '    End Function

    '    Public Overridable Function GetSqlDelete(dBObject As IDBObject) As String Implements IDBObjectDescriptor.GetSqlDelete
    '        Dim ret As String = ""
    '        With DirectCast(dBObject, DBSchema)
    '            ret =
    '<string>GO
    'DROP SCHEMA <%= .Name & vbNewLine %></string>.Value

    '        End With
    '        Return ret
    '    End Function


End Class

