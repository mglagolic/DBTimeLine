Imports MRFramework.MRPersisting.Core
' DataLayerObject
Public Class MRDLO
    Implements IMRDLO


    'Public Property WritableColumns As New List(Of String) Implements IMRDLO.WritableColumns
    'Public Property ColumnValues As New MRRowColumnValues Implements IMRDLO.ColumnValues
    Public Property ColumnValues As New Dictionary(Of String, Object) Implements IMRDLO.ColumnValues

    Public Function Clone() As Object Implements IMRDLO.Clone
        Dim ret As New MRDLO
        If Me.ColumnValues IsNot Nothing Then
            For Each columnName As String In Me.ColumnValues.Keys
                If TypeOf Me.ColumnValues(columnName) Is String Then
                    ret.ColumnValues.Add(columnName, String.Copy(CStr(Me.ColumnValues(columnName))))
                Else
                    ret.ColumnValues.Add(columnName, Me.ColumnValues(columnName))
                End If
            Next
        Else
            ret.ColumnValues = Nothing
        End If
        'If Me.WritableColumns IsNot Nothing Then
        '    For Each columnName As String In Me.WritableColumns
        '        ret.WritableColumns.Add(String.Copy(columnName))
        '    Next
        'Else
        '    ret.WritableColumns = Nothing
        'End If

        Return ret
    End Function

    
End Class
