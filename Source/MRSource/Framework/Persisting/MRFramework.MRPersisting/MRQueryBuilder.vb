Imports MRFramework.MRCore
Imports MRFramework.MRCore.Enums
Imports MRFramework.MRPersisting.Core

Namespace MRQueryBuilder


    Public Module MRQueryBuilder

#Region "Reader"

        Public Function GetSQL(sql As String, where As String, Optional preSql As String = "") As String
            Dim ret As String = ""
            Dim whr As String = where
            If whr Is Nothing Then
                whr = ""
            End If

            ret &= _
    <string><%= preSql %>
SELECT 
    *
FROM
(
<%= sql %>
) as mainSql
    <%= IIf(whr.Trim.Length > 0, vbNewLine & "WHERE ", "") %>
        <%= vbNewLine & whr %>
    </string>.Value


            Return ret
        End Function

        Public Function GetOrderByClause(orderItems As List(Of IMROrderItem)) As String
            Dim str As String = ""

            For Each orderItem As IMROrderItem In orderItems
                str &= orderItem.ColumnName & IIf(orderItem.Direction = MRCore.Enums.eOrderDirection.Ascending, " ASC", " DESC").ToString & ", "
            Next
            Return str.TrimEnd.TrimEnd(","c)
        End Function

        Public Function GetRowNumberClause(orderItems As List(Of IMROrderItem)) As String
            Return "row_number() over (order by  " & GetOrderByClause(orderItems) & ") - 1" ' - 1 treba radi zero based indexa
        End Function
        Public Function GetQuery_FillAll(mainSql As String, getRowNumberClause As String, primaryKey() As DataColumn, ByVal includeRowNum As Boolean, Optional preSql As String = "") As String
            Dim sql As String = ""

            sql = _
    <string><%= preSql %>
select 
    <%= IIf(includeRowNum, "RowNum,", "") %>
    data.*
from
(
select 
    <%= getRowNumberClause %> RowNum,
    <%= primaryKey(0).ColumnName %>
from
(
<%= mainSql %>
) as Query
) as main
inner join 
(
<%= mainSql %>
) as Data on main.<%= primaryKey(0).ColumnName %> = data.<%= primaryKey(0).ColumnName %></string>.Value

            Return sql
        End Function

        Public Function GetQuery_TotalRowCount(getQuery_FillAllClause As String) As String
            Dim sql As String = _
    <string>select 
    count(*) 
from (
<%= getQuery_FillAllClause %>
) as count</string>.Value

            Return sql
        End Function


        Private Function GetMinRowNum(pageindex As Integer, pageSize As Integer, totalRows As Integer) As Integer
            Return pageindex * pageSize
        End Function
        Private Function GetMaxRowNum(pageindex As Integer, pageSize As Integer, totalRows As Integer) As Integer
            Return (pageindex + 1) * pageSize
        End Function

        Public Function GetQuery_Page(queryFillAll As String, pageIndex As Integer, pageSize As Integer, totalRowCount As Integer, Optional pagingEnabled As Boolean = True) As String
            Dim ret As String = queryFillAll
            If pagingEnabled Then
                Dim whr As String = "rownum >= " & GetMinRowNum(pageIndex, pageSize, totalRowCount).ToString & " and rowNum < " & GetMaxRowNum(pageIndex, pageSize, totalRowCount).ToString
                ret &= vbNewLine & "WHERE " & vbNewLine & whr
            End If
            Return ret
        End Function

        Public Function GetQuery_MainOrderBy(query As String) As String
            Dim ret As String = query
            ret &= <string>
ORDER BY
    RowNum</string>.Value
            Return ret
        End Function

#End Region

#Region "Writer"

#End Region

    End Module

End Namespace
