Public Class DBSqlRevision
    Public Property Created As Date
    Public Property Granulation As Integer
    Public Property DBObjectFullName As String

    Public Property DBObjectType As eDBObjectType
    Public ReadOnly Property DBObjectTypeName As String
        Get
            Return [Enum].GetName(GetType(eDBObjectType), DBObjectType)
        End Get
    End Property

    Public Property DBRevisionType As eDBRevisionType
    Public ReadOnly Property DBRevisionTypeName As String
        Get
            Return [Enum].GetName(GetType(eDBRevisionType), DBRevisionType)
        End Get
    End Property

    Public Property Parent As DBRevision

    Public Shared Function CompareRevisionsForDbCreations(rev1 As DBSqlRevision, rev2 As DBSqlRevision)
        Dim ret As Integer = 0

        If ret = 0 Then
            ret = rev1.Created.CompareTo(rev2.Created)
        End If
        If ret = 0 Then
            ret = rev1.Granulation.CompareTo(rev2.Granulation)
        End If
        If ret = 0 Then
            ret = rev1.DBObjectType.CompareTo(rev2.DBObjectType)
        End If
        If ret = 0 Then
            ret = rev1.DBRevisionType.CompareTo(rev2.DBRevisionType)
        End If
        If ret = 0 Then
            ret = rev1.DBObjectFullName.CompareTo(rev2.DBObjectFullName)
        End If

        Return ret
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim ret As Boolean = False

        Dim compareResult As Integer = DBSqlRevision.CompareRevisionsForDbCreations(Me, DirectCast(obj, DBSqlRevision))
        If compareResult = 0 Then
            ret = True
        End If

        Return ret
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Created.GetHashCode Xor Granulation.GetHashCode Xor DBObjectFullName.GetHashCode Xor DBObjectType.GetHashCode Xor DBRevisionType.GetHashCode
    End Function

End Class
