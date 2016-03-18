Public MustInherit Class DBObject
    Implements IDBObject

#Region "IDBObject"
    Public Property Name As String Implements IDBObject.Name

    Public Property Parent As IDBChained Implements IDBChained.Parent

    Private ReadOnly Property _Revisions As New List(Of DBRevision)
    Public ReadOnly Property Revisions As List(Of DBRevision) Implements IDBObject.Revisions
        Get
            Return _Revisions
        End Get
    End Property

    Public MustOverride ReadOnly Property DBObjectType As eDBObjectType Implements IDBObject.DBObjectType

    Public ReadOnly Property DBCreator As DBCreator Implements IDBObject.DBCreator
        Get
            Dim ret As DBCreator = Nothing

            Dim p As IDBChained = Me
            While p IsNot Nothing
                If TypeOf p Is DBCreator Then
                    ret = CType(p, DBCreator)
                    Exit While
                Else
                    p = p.Parent
                End If
            End While

            Return ret
        End Get
    End Property

    Public ReadOnly Property SchemaName As String Implements IDBObject.SchemaName
        Get
            Dim ret As String = String.Empty

            Dim p As IDBChained = Me
            While p IsNot Nothing
                If TypeOf p Is DBSchema Then
                    ret = DirectCast(p, DBSchema).Name
                    Exit While
                Else
                    p = p.Parent
                End If
            End While

            Return ret
        End Get
    End Property

    Public Function AddRevision(revision As DBRevision, Optional descriptor As IDBObjectDescriptor = Nothing) As DBRevision Implements IDBObject.AddRevision
        Revisions.Add(revision)

        revision.Parent = Me

        If descriptor IsNot Nothing Then
            ApplyDescriptor(descriptor)
        End If

        DBCreator.SourceDBSqlRevisions.Add(New DBSqlRevision(revision))

        Return revision
    End Function
    Public Function FindRevision(created As Date, granulation As Integer) As DBRevision
        Dim ret As DBRevision = Nothing
        ret = Revisions.Find(Function(rev) (rev.Created = created AndAlso rev.Granulation = granulation))

        Return ret
    End Function

    Private sbFullName As New Text.StringBuilder("")
    Public Function GetFullName() As String Implements IDBObject.GetFullName
        sbFullName.Clear()

        Dim p As IDBObject = Me
        While p IsNot Nothing
            sbFullName.Insert(0, p.Name & ".")
            If TypeOf p.Parent Is IDBObject Then
                p = CType(p.Parent, IDBObject)
            Else
                p = Nothing
            End If
        End While

        Return sbFullName.ToString().TrimEnd("."c)
    End Function

    MustOverride Sub ApplyDescriptor(descriptor As IDBObjectDescriptor)
    MustOverride Function GetDescriptor() As IDBObjectDescriptor Implements IDBObject.GetDescriptor
    MustOverride Function GetSqlCreate() As String Implements IDBObject.GetSqlCreate
    MustOverride Function GetSqlModify() As String Implements IDBObject.GetSqlModify
    MustOverride Function GetSqlDelete() As String Implements IDBObject.GetSqlDelete


#End Region

End Class
