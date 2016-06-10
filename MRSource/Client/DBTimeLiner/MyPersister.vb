Imports Framework.Persisting
Imports Framework.Persisting.Interfaces
Imports MRFramework.MRPersisting.Factory

Public Class MyPersister
    Inherits Persister

    Protected Overrides ReadOnly Property DataBaseTableName As String
        Get
            Return "Place.Table1"
        End Get
    End Property

    Protected Overrides ReadOnly Property Sql As String
        Get
            Return _
"SELECT
	t1.ID,
    Table2Naziv = t2.Naziv,
    Table2ID = t2.TableKey
FROM
	Place.Table1 t1
	LEFT JOIN Place.Table2 t2 on t1.Table2Key = t2.TableKey"
        End Get
    End Property
End Class

Public Class Tests
    Private Sub persistingTests()

        Dim per As IPersister = New MyPersister
        per.CNN = MRC.GetConnection
        per.Where = "t1.Broj = 1"
        per.OrderItems.Add(New Implementation.OrderItem() With {.SqlName = "t1.Broj"})
        per.OrderItems.Add(New Implementation.OrderItem() With {.SqlName = "t1.id"})
        per.CNN.Open()
        Dim ts1 = New TimeSpan(Now.Ticks)
        Dim data = per.GetData(Nothing)
        Dim ts2 = New TimeSpan(Now.Ticks)
        Dim dataPage = per.GetData(Nothing, 20)
        Dim ts3 = New TimeSpan(Now.Ticks)
        per.PageSize = 30000
        Dim totalPages As Integer = CInt(data.Count / per.PageSize)
        For i As Integer = 1 To totalPages
            per.GetData(Nothing, i)
        Next
        Dim ts4 = New TimeSpan(Now.Ticks)

        Console.WriteLine((ts2 - ts1).ToString & ":" & data.Count.ToString())
        Console.WriteLine((ts3 - ts2).ToString & ":" & dataPage.Count.ToString())
        Console.WriteLine((ts4 - ts2).ToString & " ukupno stranica:" & totalPages.ToString())
        per.CNN.Close()
        Dim o = data.Single(Function(itm) CType(itm.ColumnValues("ID"), Guid) = New Guid("3aca7383-5ae4-4a17-b9b0-000a761a505c"))

    End Sub

End Class
