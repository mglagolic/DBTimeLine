Public MustInherit Class DBObject
    Implements IDBObject

#Region "IDBObject"
    Public Property Name As String Implements IDBObject.Name

    Public Property Parent As IDBChained Implements IDBChained.Parent
    Public Property Descriptor As IDBObjectDescriptor Implements IDBObject.Descriptor
    Public ReadOnly Property DBObjects As New Dictionary(Of String, IDBObject) Implements IDBObject.DBObjects
    Public ReadOnly Property Revisions As New List(Of IDBRevision) Implements IDBObject.Revisions

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

    Public Function AddRevision(revision As IDBRevision, Optional descriptor As IDBObjectDescriptor = Nothing) As IDBRevision Implements IDBObject.AddRevision
        Revisions.Add(revision)

        revision.Parent = Me

        If descriptor IsNot Nothing Then
            Me.Descriptor = descriptor
        End If

        DBCreator.SourceDBSqlRevisions.Add(New DBSqlRevision(revision))

        Return revision
    End Function
    Public Function FindRevision(created As Date, granulation As Integer) As IDBRevision Implements IDBObject.FindRevision
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

    MustOverride Function GetSqlCreate() As String Implements IDBObject.GetSqlCreate
    MustOverride Function GetSqlModify() As String Implements IDBObject.GetSqlModify
    MustOverride Function GetSqlDelete() As String Implements IDBObject.GetSqlDelete


#End Region

End Class
