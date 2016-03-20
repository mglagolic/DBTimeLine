Imports MRFramework
Imports MRFramework.MRPersisting.Core

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
    Public Property Parent As IDBRevision
    Public Property SchemaName As String
    Public Property DBObjectName As String
    Public Property Sql As String
    Public Property Description As String

    Public Sub New(dBRevision As IDBRevision)
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
            Sql = .GetSql
            Description = Sql
            Parent = dBRevision
        End With
    End Sub
    Public Sub New(dlo As IMRDLO, dBCreator As DBCreator)
        Created = CDate(dlo.ColumnValues("Created"))
        DBObjectFullName = CStr(dlo.ColumnValues("DBObjectFullName"))
        DBObjectType = CType([Enum].Parse(GetType(eDBObjectType), CStr(dlo.ColumnValues("DBObjectType"))), eDBObjectType)
        DBRevisionType = CType([Enum].Parse(GetType(eDBRevisionType), CStr(dlo.ColumnValues("DBRevisionType"))), eDBRevisionType)
        Granulation = CInt(dlo.ColumnValues("Granulation"))
        DBObjectName = CStr(dlo.ColumnValues("DBObjectName"))
        SchemaName = CStr(dlo.ColumnValues("SchemaName"))
        Parent = FindParent(dBCreator)
    End Sub

    Private Function FindParent(dBCreator As DBCreator) As IDBRevision
        Dim ret As IDBRevision = Nothing
        Dim dbObject As IDBObject = Nothing
        Select Case DBObjectType
            Case eDBObjectType.Field
                If dBCreator.DBFields.ContainsKey(DBObjectName) Then
                    dbObject = dBCreator.DBFields(DBObjectName)
                End If

            Case eDBObjectType.Table
                If dBCreator.DBTables.ContainsKey(DBObjectName) Then
                    dbObject = dBCreator.DBTables(DBObjectName)
                End If
        End Select
        If dbObject IsNot Nothing Then
            ret = dbObject.FindRevision(Created, Granulation)
        End If

        Return ret
    End Function

    Public Function GetDlo() As IMRDLO
        Dim dlo As New MRPersisting.MRDLO
        With dlo.ColumnValues
            .Add("Created", Created)
            .Add("DBObjectFullName", DBObjectFullName)
            .Add("DBObjectType", DBObjectTypeName)
            .Add("DBRevisionType", DBRevisionTypeName)
            .Add("Granulation", Granulation)
            .Add("DBObjectName", DBObjectName)
            .Add("SchemaName", SchemaName)
            .Add("Description", Description)
        End With
        Return dlo
    End Function



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

#Region "EqualityComparer"
    Public Class DBSqlRevisionEqualityComparer
        Implements IEqualityComparer(Of DBSqlRevision)

        Public Shadows Function Equals(x As DBSqlRevision, y As DBSqlRevision) As Boolean Implements IEqualityComparer(Of DBSqlRevision).Equals
            Dim ret As Boolean = False

            Dim compareResult As Integer = DBSqlRevision.CompareRevisionsForDbCreations(x, y)
            If compareResult = 0 Then
                ret = True
            End If

            Return ret
        End Function

        Public Shadows Function GetHashCode(obj As DBSqlRevision) As Integer Implements IEqualityComparer(Of DBSqlRevision).GetHashCode
            With obj
                Return .Created.GetHashCode Xor .Granulation.GetHashCode Xor .DBObjectFullName.GetHashCode Xor .DBObjectType.GetHashCode Xor .DBRevisionType.GetHashCode
            End With
        End Function
    End Class

#End Region

#Region "Persister"

    Public Class DBSqlRevisionPersister
        Inherits MRPersisting.MRPersister

        Public Overrides ReadOnly Property DataBaseTableName As String
            Get
                Return "DBCreator.Revision"
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
