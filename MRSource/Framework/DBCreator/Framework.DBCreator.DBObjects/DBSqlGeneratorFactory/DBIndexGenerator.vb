Option Strict On

Public Class DBIndexGenerator
    Implements IDBIndexGenerator

    Public Property Parent As IDBSqlGenerator Implements IDBIndexGenerator.Parent

    Public Function GetSqlCreate(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlCreate
        Dim ret As String = ""

        Dim desc As IDBIndexDescriptor = CType(dbObject.Descriptor, IDBIndexDescriptor)

        Dim indexName As String = desc.IndexName
        Dim strUnique As String = ""
        Dim strClustered As String = "NONCLUSTERED"
        Dim strKeys As String = ""
        Dim strInclude As String = ""
        Dim sb As New Text.StringBuilder()

        If String.IsNullOrWhiteSpace(indexName) Then
            With DirectCast(dbObject, IDBIndex)
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

        With CType(dbObject, IDBIndex)
            ret = String.Format("CREATE {0} {1} INDEX {2} ON {3} ({4}) {5}", strUnique, strClustered, indexName, DirectCast(dbObject.Parent, IDBObject).GetFullName, strKeys, strInclude)
        End With

        Return ret
    End Function

    Public Function GetSqlDelete(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlDelete
        Dim ret As String = ""

        Dim desc As IDBIndexDescriptor = CType(dbObject.Descriptor, IDBIndexDescriptor)

        Dim indexName As String = desc.IndexName
        If String.IsNullOrWhiteSpace(indexName) Then
            With DirectCast(dbObject, IDBIndex)
                indexName = desc.GetIndexName(.SchemaName, DirectCast(.Parent, IDBObject).Name)
            End With
        End If

        ret = String.Format("DROP INDEX {0} ON {1}
", indexName, DirectCast(dbObject.Parent, IDBObject).GetFullName)

        Return ret
    End Function

    Public Function GetSqlModify(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlModify
        Throw New NotImplementedException
    End Function
End Class
