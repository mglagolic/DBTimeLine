Imports System.ComponentModel

Public Interface IMRDataPaging
    Property PagingEnabled As Boolean
    Property PageRowCount As Integer
    ReadOnly Property RowCount As Integer
    ' prebaciti u mrdatagridview

    'Property CurrentPage As MRDataPage 
    'Property CurrentPageIndex As Integer
    'Function PageLoadNext() As MRDataPage
    'Function PageLoadPrevious() As MRDataPage

    Function PageLoadIndex(ByVal i As Integer) As MRDataPage
    Function PageLoadFirst() As MRDataPage
    Function PageLoadLast() As MRDataPage

    Property Pages As List(Of MRDataPage)
    Sub InitializePages()

    Event PageLoading(ByVal sender As Object, e As MREventArgs.MRPagingCancelEventArgs)
    Event PageLoaded(ByVal sender As Object, ByVal e As MREventArgs.MRPagingEventArgs)
    Event PagesInitializing(ByVal sender As Object, ByVal e As CancelEventArgs)
    Event PagesInitialized(ByVal sender As Object, ByVal e As EventArgs)


End Interface
