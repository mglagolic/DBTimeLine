Public Enum eDBRevisionType
    Create = 0
    Modify = 1
    Delete = 2
    AlwaysExecute = 3
End Enum

Public Class DBRevision

    Public Property DBRevisionType As eDBRevisionType
    Public Property Parent As IDBObject

    Public Property Created As Date
    Public Property Granulation As Integer
    Public Property DBObject As IDBObject

    Private Sub New()

    End Sub

    Public Sub New(created As Date, granulation As Integer, ByVal dBRevisionType As eDBRevisionType, Optional dbObject As IDBObject = Nothing)
        MyClass.New()
        Me.DBRevisionType = dBRevisionType
        Me.Created = created
        Me.DBObject = dbObject
        Me.Granulation = granulation
        If Me.DBObject IsNot Nothing Then
            Me.DBObject.Parent = Parent
            Parent = Me.DBObject
        End If
    End Sub

    Public Sub New(revision As DBRevision)
        MyClass.New(revision.Created, revision.Granulation, revision.DBRevisionType, revision.DBObject)
    End Sub

    Private sbParentFullName As New System.Text.StringBuilder(parentFullName)
    Private parentFullName As String = ""
    Private Function GetParentFullName() As String
        parentFullName = String.Empty
        Dim p As IDBObject = Parent
        While p IsNot Nothing
            sbParentFullName.Insert(0, p.Name & ".")
            p = p.Parent
        End While
        Return sbParentFullName.ToString().TrimEnd("."c)
    End Function

    Public Function ConvertToSqlRevision() As DBSqlRevision
        Dim ret As New DBSqlRevision

        With ret
            .Created = Created
            .DBObjectFullName = GetParentFullName()
            .DBObjectType = [Enum].GetName(GetType(eDBObjectType), Parent.DBObjectType)
            .DBRevisionType = [Enum].GetName(GetType(eDBRevisionType), DBRevisionType)
            .Granulation = Granulation
        End With

        Return ret
    End Function

End Class
