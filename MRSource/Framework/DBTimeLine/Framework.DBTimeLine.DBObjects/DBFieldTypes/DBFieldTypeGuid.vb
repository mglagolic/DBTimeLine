Public Class DBFieldTypeGuid
    Inherits DBFieldType

    Public Overrides Function GetFieldTypeSql(fieldDescriptor As IDBFieldDescriptor, dBType As eDBType) As String
        Dim ret As String = "UNIQUEIDENTIFIER"

        Return ret
    End Function
End Class
