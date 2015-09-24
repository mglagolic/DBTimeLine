Imports System.ComponentModel
Imports System.Data.Common

Public Interface IMRDatatable
    ' prebaciti u MRDatagridview
    'ReadOnly Property CurrentRow As DataRow
    ReadOnly Property TotalRowCount As Integer
    Property CNN As DbConnection
    Property UseSchemaCaching As Boolean
    Property UsePrimaryKeyCaching As Boolean


    Function FillAll() As Integer

    'Function MoveToIndex(ByVal i As Integer) As DataRow
    'Function MoveFirst() As DataRow
    'Function MoveLast() As DataRow
    'Function MoveNext() As DataRow
    'Function MovePrevious() As DataRow

    ReadOnly Property Rows As DataRowCollection
    ReadOnly Property OrderItems As List(Of MRConfig.IMROrderItem)

    Function SaveChanges(ByVal transaction As DbTransaction) As Integer

    Event FillingAll(ByVal sender As Object, ByVal e As CancelEventArgs)
    Event FilledAll(ByVal sender As Object, ByVal e As EventArgs)

    'Event Moving(ByVal sender As IMRDataSource, ByVal ce As CancelEventArgs)
    'Event Moved(ByVal sender As IMRDataSource, ByVal ce As EventArgs)

End Interface

