Imports MRFramework.MRPersisting.Core

Public Class DBSqlRevision

    Public Sub New(dBRevision As DBRevision)
        With dBRevision
            Created = .Created
            DBObjectFullName = .Parent.GetFullName
            DBObjectName = .Parent.Name
            DBObjectType = .Parent.DBObjectType
            DBRevisionType = .DBRevisionType
            Granulation = .Granulation
            If .Parent IsNot Nothing Then
                SchemaName = .Parent.SchemaName
            End If
            Parent = dBRevision
        End With
    End Sub
    Public Sub New(dlo As IMRDLO)
        Created = CDate(dlo.ColumnValues("Created"))
        DBObjectFullName = CStr(dlo.ColumnValues("DBObjectFullName"))
        DBObjectType = CType([Enum].Parse(GetType(eDBObjectType), CStr(dlo.ColumnValues("DBObjectType"))), eDBObjectType)
        DBRevisionType = CType([Enum].Parse(GetType(eDBRevisionType), CStr(dlo.ColumnValues("DBRevisionType"))), eDBRevisionType)
        Granulation = CInt(dlo.ColumnValues("Granulation"))
        DBObjectName = CStr(dlo.ColumnValues("DBObjectName"))
        SchemaName = CStr(dlo.ColumnValues("SchemaName"))
        'TODO - ovdje se parent ne postavlja, treba li ovo?
    End Sub

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
    Public Property SchemaName As String
    Public Property DBObjectName As String


    Public Shared Function CompareRevisionsForDbCreations(rev1 As DBSqlRevision, rev2 As DBSqlRevision) As Integer
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

#Region "Persister"

    Public Class DBSqlRevisionPersister
        Inherits MRPersisting.MRPersister

        Public Overrides ReadOnly Property DataBaseTableName As String
            Get
                Return "Common.Revision"
            End Get
        End Property
        Public Overrides ReadOnly Property SQL As String
            Get
                Return "SELECT ID, Created, Granulation, DBObjectType, DBRevisionType, DBObjectFullName, DBObjectName, SchemaName FROM " & DataBaseTableName
            End Get
        End Property
    End Class

#End Region

End Class
