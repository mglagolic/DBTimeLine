Public Class DBFieldTypeInteger
    Inherits DBFieldType

    Public Overrides Function GetFieldTypeSql(fieldDescriptor As IDBFieldDescriptor, dBType As eDBType) As String
        Dim ret As String = "INTEGER"

        Return ret
    End Function
End Class
