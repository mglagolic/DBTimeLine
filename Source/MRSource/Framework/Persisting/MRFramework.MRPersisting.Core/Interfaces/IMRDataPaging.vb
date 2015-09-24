Imports System.Data.Common
Public Interface IMRDataPaging

    Property PagingEnabled As Boolean
    Property PageSize As Integer
    Sub GettingDataPage(pageIndex As Integer, Optional transaction As DbTransaction = Nothing)
    Function GetDataPage(pageIndex As Integer, Optional transaction As DbTransaction = Nothing) As Dictionary(Of Object, IMRDLO)


End Interface

