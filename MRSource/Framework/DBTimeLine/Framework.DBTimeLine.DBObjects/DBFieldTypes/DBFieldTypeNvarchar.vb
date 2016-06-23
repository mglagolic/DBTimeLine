Public Class DBFieldTypeNvarchar
    Inherits DBFieldType

    Public Overrides Function GetFieldTypeSql(fieldDescriptor As IDBFieldDescriptor, dBType As eDBType) As String
        Dim ret As String

        ret = "NVARCHAR(" & CStr(IIf(fieldDescriptor.Size = -1, "MAX", fieldDescriptor.Size.ToString)) & ")"

        Return ret
    End Function
End Class
