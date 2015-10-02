Imports System.Runtime.Serialization

<DataContract> _
Public Class MROrderItem
    Implements MRFramework.MRCore.IMROrderItem

    Public Sub New()

    End Sub

    Public Sub New(columnName As String, direction As MRCore.Enums.eOrderDirection)
        MyClass.New()

        Me.ColumnName = columnName
        Me.Direction = direction
    End Sub



    <DataMember(IsRequired:=True)> _
    Public Property ColumnName As String Implements MRFramework.MRCore.IMROrderItem.ColumnName

    <DataMember(IsRequired:=True)> _
    Public Property Direction As MRFramework.MRCore.Enums.eOrderDirection Implements MRFramework.MRCore.IMROrderItem.Direction

End Class
