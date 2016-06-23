Public Class DBFieldTypeDecimal
    Inherits DBFieldType

    Public Overrides Function GetFieldTypeSql(fieldDescriptor As IDBFieldDescriptor, dBType As eDBType) As String
        Dim ret As String = "DECIMAL(" & fieldDescriptor.Size.ToString & ", " & fieldDescriptor.Precision.ToString & ")"

        Return ret
    End Function
End Class
