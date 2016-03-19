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

    Public Overrides Function GetDBObjectInstance() As IDBObject
        Return New myField(Me)
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
    Public Overrides Sub ApplyDescriptor(descriptor As IDBObjectDescriptor)
        MyBase.ApplyDescriptor(descriptor)

        NekiNoviKojegNemaUGetDescriptoru = DirectCast(descriptor, myfieldDesc).NekiNoviKojegNemaUGetDescriptoru
    End Sub
    Public Property NekiNoviKojegNemaUGetDescriptoru As String

    Public Overrides Function GetDescriptor() As IDBObjectDescriptor
        Dim desc As DBFieldDescriptor = CType(MyBase.GetDescriptor(), DBFieldDescriptor)
        Dim ret As New myfieldDesc With {.DefaultValue = desc.DefaultValue, .FieldType = desc.FieldType, .IsIdentity = desc.IsIdentity, .Nullable = desc.Nullable, .Precision = .Precision, .Size = desc.Size, .NekiNoviKojegNemaUGetDescriptoru = NekiNoviKojegNemaUGetDescriptoru}

        Return ret
    End Function
End Class