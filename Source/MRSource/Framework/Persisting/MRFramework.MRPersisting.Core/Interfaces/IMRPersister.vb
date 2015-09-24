Imports MRFramework.MRPersisting.Core
Imports System.Data.Common

Public Interface IMRPersister
    Inherits IDisposable
    Inherits IMRDataPaging

    Property CNN As DbConnection

#Region "DataReader"
    ReadOnly Property OrderItems As List(Of mrframework.MRCore.IMROrderItem)
    Property Where As String


    Function GetTotalRowCount(Optional transaction As DbTransaction = Nothing) As Integer
    Sub GettingData(Optional transaction As DbTransaction = Nothing)

    Function GetData(Optional transaction As DbTransaction = Nothing) As Dictionary(Of Object, IMRDLO)

    Event RowReading(sender As Object, e As IMRRowReadingEventArgs)
    Event RowRead(sender As Object, e As IMRRowReadEventArgs)
#End Region

#Region "DataWriter"
    Function Insert(dlo As MRPersisting.Core.IMRDLO, transaction As DbTransaction) As IMRInsertDLOReturnValue
    Function InsertBulk(lsDlo As List(Of MRPersisting.Core.IMRDLO), transaction As DbTransaction) As List(Of IMRInsertDLOReturnValue)
    Function Update(dlo As MRPersisting.Core.IMRDLO, transaction As DbTransaction, Optional lastWins As Boolean = False) As IMRUpdateDLOReturnValue
    Function Delete(dlo As MRPersisting.Core.IMRDLO, transaction As DbTransaction, Optional lastWins As Boolean = False) As IMRDeleteDLOReturnValue



#End Region
End Interface
