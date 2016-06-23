Public Class DBFieldTypeBoolean
    Inherits DBFieldType

    Public Overrides Function GetFieldTypeSql(fieldDescriptor As IDBFieldDescriptor, dBType As eDBType) As String
        Dim ret As String = "BIT"

        Return ret
    End Function
End Class
