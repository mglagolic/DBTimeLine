Imports System.Data.Common
Imports System.ComponentModel

Public Interface IMRDataReader
    'Property CNN As DbConnection

    ReadOnly Property OrderItems As List(Of MRConfig.IMROrderItem)
    ReadOnly Property MainWhere As String
    Property Where As String
    
    'Function GetFillAll(Optional transaction As DbTransaction = Nothing) As DbDataReader
    Function GetTotalRowCount(Optional transaction As DbTransaction = Nothing) As Integer
    Function GetData(Optional transaction As DbTransaction = Nothing) As List(Of IMRReadableObject)


    'Event FillingAll(ByVal sender As Object, ByVal e As CancelEventArgs)
    'Event FilledAll(ByVal sender As Object, ByVal e As EventArgs)

    Event RowReading(sender As Object, e As MREventArgs.MRRowReadingEventArgs)
    Event DataColumnRead(sender As Object, e As MREventArgs.MRDataColumnReadEventArgs)
    Event RowRead(sender As Object, e As MREventArgs.MRRowReadEventArgs)



End Interface
