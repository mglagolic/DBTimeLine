Public Class DBIndex
    Inherits DBObject
    Implements IDBIndex

    Public Sub New()

    End Sub

    Public Overrides ReadOnly Property ObjectTypeOrdinal As Integer
        Get
            Return 50
        End Get
    End Property

    Public Overrides ReadOnly Property ObjectTypeName As String
        Get
            Return "Index"
        End Get
    End Property

    Public Overrides Function GetSqlCreate(dBType As eDBType) As String
        Dim ret As String = ""

        Dim desc As IDBIndexDescriptor = CType(Descriptor, IDBIndexDescriptor)

        Dim indexName As String = desc.IndexName
        Dim strUnique As String = ""
        Dim strClustered As String = "NONCLUSTERED"
        Dim strKeys As String = ""
        Dim strInclude As String = ""
        Dim sb As New Text.StringBuilder()

        If String.IsNullOrWhiteSpace(indexName) Then
            With Me
                indexName = desc.GetIndexName(.SchemaName, DirectCast(.Parent, IDBObject).Name)
            End With
        End If

        If desc.Clustered Then
            strClustered = "CLUSTERED"
        End If
        If desc.Unique Then
            strUnique = "UNIQUE"
        End If

        For i As Integer = 0 To desc.Keys.Count - 1
            sb.Append(desc.Keys(i))
            sb.Append(",")
        Next
        sb.Length -= 1
        strKeys = sb.ToString
        sb.Clear()

        If desc.Include.Count > 0 Then
            sb.Append("INCLUDE (")
            For i As Integer = 0 To desc.Include.Count - 1
                sb.Append(desc.Include(i))
                sb.Append(",")
            Next
            sb.Length -= 1
            sb.Append(")")
            strInclude = sb.ToString
            sb.Clear()
        End If

        With Me
            ret = String.Format("CREATE {0} {1} INDEX {2} ON {3} ({4}) {5}", strUnique, strClustered, indexName, DirectCast(Parent, IDBObject).GetFullName, strKeys, strInclude)
        End With

        Return ret
    End Function

    Public Overrides Function GetSqlModify(dBType As eDBType) As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetSqlDelete(dBType As eDBType) As String
        Dim ret As String = ""

        Dim desc As IDBIndexDescriptor = CType(Descriptor, IDBIndexDescriptor)

        Dim indexName As String = desc.IndexName
        If String.IsNullOrWhiteSpace(indexName) Then

            indexName = desc.GetIndexName(SchemaName, DirectCast(Parent, IDBObject).Name)

        End If

        ret = String.Format("DROP INDEX {0} ON {1}
", indexName, DirectCast(Parent, IDBObject).GetFullName)

        Return ret
    End Function
End Class
