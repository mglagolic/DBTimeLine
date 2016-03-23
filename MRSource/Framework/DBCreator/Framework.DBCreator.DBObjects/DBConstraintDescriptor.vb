Imports Framework.DBCreator

Public Class DBConstraintDescriptor
    Implements IDBConstraintDescriptor

    Public Property ConstraintType As eDBConstraintType Implements IDBConstraintDescriptor.ConstraintType

    Public Overridable Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBConstraint() With {.Parent = parent, .Descriptor = Me}
    End Function

    Public Sub New()

    End Sub

    Public Sub New(descriptor As IDBObjectDescriptor)
        MyClass.New()

        With CType(descriptor, IDBConstraintDescriptor)
            ConstraintType = .ConstraintType
        End With
    End Sub

End Class