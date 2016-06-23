Public Class DBFieldTypeDatetime
    Inherits DBFieldType

    Public Overrides Function GetFieldTypeSql(fieldDescriptor As IDBFieldDescriptor, dBType As eDBType) As String
        Dim ret As String = "DATETIME"

        Return ret
    End Function
End Class
