Option Strict On

Imports Framework.DBCreator
Imports Framework.DBCreator.DBObjects

Public Class myfieldDesc
    Inherits DBFieldDescriptor

    Public Property NekiNoviKojegNemaUGetDescriptoru As String

    Public Sub New()
        MyBase.New
    End Sub
    Public Sub New(descriptor As myfieldDesc)
        MyBase.New(descriptor)

        NekiNoviKojegNemaUGetDescriptoru = descriptor.NekiNoviKojegNemaUGetDescriptoru
    End Sub

    Public Overrides Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject
        Return New myField(Me) With {.Parent = parent}
    End Function
End Class

Public Class myField
    Inherits DBField

    Public Overrides Function GetSqlCreate() As String
        Dim sql As String = MyBase.GetSqlCreate()
        sql &= NekiNoviKojegNemaUGetDescriptoru

        Return sql
    End Function

    Public Sub New(descriptor As myfieldDesc)
        MyBase.New(descriptor)
    End Sub

    Public Property NekiNoviKojegNemaUGetDescriptoru As String


End Class