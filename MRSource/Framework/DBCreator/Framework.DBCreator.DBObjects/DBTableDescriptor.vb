Public Class DBTableDescriptor
    Implements IDBTableDescriptor

    Public Overridable Function GetDBObjectInstance() As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBTable(Me)
    End Function

    Public Property CreatorFieldDescriptor As IDBFieldDescriptor Implements IDBTableDescriptor.CreatorFieldDescriptor
    Public Property CreatorFieldName As String Implements IDBTableDescriptor.CreatorFieldName

    '    Public Function GetSqlCreate(dBObject As IDBObject) As String Implements IDBObjectDescriptor.GetSqlCreate
    '        Dim ret As String = ""
    '        With DirectCast(dBObject, DBTable)
    '            ret =
    '<string>
    'CREATE TABLE <%= .SchemaName %>.<%= .Name %> 
    '(
    '    <%= CreatorFieldName & " " %><%= GetFieldTypeSql %>
    ')
    '</string>.Value
    '        End With

    '        Return ret
    '    End Function

    '    Public Function GetSqlDelete(dBObject As IDBObject) As String Implements IDBObjectDescriptor.GetSqlDelete
    '        Dim ret As String = ""
    '        With DirectCast(dBObject, DBTable)
    '            ret =
    '<string>
    'DROP TABLE <%= .SchemaName %>.<%= .Name %>
    '</string>.Value
    '        End With

    '        Return ret
    '    End Function

    '    Public Function GetSqlModify(dBObject As IDBObject) As String Implements IDBObjectDescriptor.GetSqlModify
    '        Throw New NotImplementedException()
    '    End Function

End Class

