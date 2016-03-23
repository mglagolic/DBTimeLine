Public MustInherit Class DBObject
    Implements IDBObject

#Region "IDBObject"
    Public Property Name As String Implements IDBObject.Name

    Public Property Parent As IDBChained Implements IDBChained.Parent
    Public Property Descriptor As IDBObjectDescriptor Implements IDBObject.Descriptor
    Public ReadOnly Property DBObjects As New Dictionary(Of String, IDBObject) Implements IDBObject.DBObjects
    Public ReadOnly Property Revisions As New List(Of IDBRevision) Implements IDBObject.Revisions

    Public MustOverride ReadOnly Property ObjectType As eDBObjectType Implements IDBObject.ObjectType

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
        Dim ret As IDBRevision = Nothing
        ret = Revisions.Find(Function(rev) (rev.Created = created AndAlso rev.Granulation = granulation))

        Return ret
    End Function


    Public Function GetFullName() As String Implements IDBObject.GetFullName
        Dim sbFullName As New Text.StringBuilder

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

    Public Overridable Function GetSqlCreate() As String Implements IDBObject.GetSqlCreate
        Dim ret As String = ""
        If TypeOf Me Is IDBSchema Then
            ret = DBCreator.DBSqlGenerator.GetSqlCreateSchema(Name)
        ElseIf TypeOf Me Is IDBTable Then
            ret = DBCreator.DBSqlGenerator.GetSqlCreateTable(SchemaName, Name, Descriptor)
        ElseIf TypeOf Me Is IDBField Then
            ret = DBCreator.DBSqlGenerator.GetSqlCreateField(SchemaName, DirectCast(Parent, IDBObject).Name, Name, Descriptor)
        Else
            Throw New NotSupportedException
        End If

        Return ret
    End Function

    Public Overridable Function GetSqlModify() As String Implements IDBObject.GetSqlModify
        Dim ret As String = ""

        If TypeOf Me Is IDBSchema Then
            Throw New NotImplementedException()
        ElseIf TypeOf Me Is IDBTable Then
            Throw New NotImplementedException()
        ElseIf TypeOf Me Is IDBField Then
            ret = DBCreator.DBSqlGenerator.GetSqlModifyField(SchemaName, DirectCast(Parent, IDBObject).Name, Name, Descriptor)
        Else
            Throw New NotSupportedException
        End If

        Return ret
    End Function

    Public Overridable Function GetSqlDelete() As String Implements IDBObject.GetSqlDelete
        Dim ret As String = ""

        If TypeOf Me Is IDBSchema Then
            ret = DBCreator.DBSqlGenerator.GetSqlDeleteSchema(Name)
        ElseIf TypeOf Me Is IDBTable Then
            ret = DBCreator.DBSqlGenerator.GetSqlDeleteTable(SchemaName, Name)
        ElseIf TypeOf Me Is IDBField Then
            ret = DBCreator.DBSqlGenerator.GetSqlDeleteField(SchemaName, DirectCast(Parent, IDBObject).Name, Name)
        Else
            Throw New NotSupportedException
        End If

        Return ret
    End Function

    ReadOnly Property ModuleKey() As String Implements IDBObject.ModuleKey
        Get
            Dim ret As String = ""

            Dim p As IDBChained = Me
            While p IsNot Nothing
                If TypeOf p Is IDBModule Then
                    ret = CType(p, IDBModule).ModuleKey
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
                If TypeOf p Is IDBSchema Then
                    ret = DirectCast(p, IDBSchema).Name
                    Exit While
                Else
                    p = p.Parent
                End If
            End While

            Return ret
        End Get
    End Property

    ReadOnly Property SchemaObjectName() As String Implements IDBObject.SchemaObjectName
        Get
            Dim ret As String = ""

            Dim p As IDBChained = Me
            While p IsNot Nothing
                If TypeOf p Is IDBTable Then
                    ret = CType(p, IDBTable).Name
                    Exit While
                Else
                    p = p.Parent
                End If
            End While

            Return ret
        End Get
    End Property

#End Region

End Class
