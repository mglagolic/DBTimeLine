Public Class MRTypeConverter
    Implements IDisposable

    Public Function GetDbType(propertyType As Type) As System.Data.DbType
        Dim ret As System.Data.DbType

        Select Case propertyType
            Case GetType(String)
                ret = DbType.String

            Case GetType(Decimal?), GetType(Decimal)
                ret = DbType.Decimal

            Case GetType(Integer?), GetType(Integer)
                ret = DbType.Int32

            Case GetType(Long?), GetType(Long)
                ret = DbType.Int64

            Case GetType(DateTime?), GetType(DateTime)
                ret = DbType.DateTime

            Case GetType(Guid)
                ret = DbType.Guid

            Case Else
                Throw New ArgumentException("TypeConverter: Unsupported propertyType.", "propertyType")
        End Select

        Return ret
    End Function


    Public Function GetDataType(dbTypeFullName As String) As Type
        Dim ret As Type

        Select Case dbTypeFullName
            Case "System.String"
                ret = GetType(String)
            Case "System.Decimal"
                ret = GetType(Decimal)
            Case "System.Int32"
                ret = GetType(Integer)
            Case "System.Int64"
                ret = GetType(Long)
            Case "System.DateTime"
                ret = GetType(DateTime)
            Case "System.Guid"
                ret = GetType(Guid)

            Case Else
                Throw New ArgumentException("TypeConverter: Unsupported dbType.", "databaseType")
        End Select

        Return ret
    End Function

    Public Function GetPropertyType(dbTypeFullName As String) As Type
        Dim ret As Type

        Select Case dbTypeFullName
            Case "System.String"
                ret = GetType(String)
            Case "System.Decimal"
                ret = GetType(Decimal)
            Case "System.Int32"
                ret = GetType(Integer)
            Case "System.Int64"
                ret = GetType(Long)
            Case "System.DateTime"
                ret = GetType(DateTime?)
            Case "System.Guid"
                ret = GetType(Guid)

            Case Else
                Throw New ArgumentException("TypeConverter: Unsupported dbType.", "databaseType")
        End Select

        Return ret
    End Function



    Public Function GetValue(value As Object) As Object
        Dim ret As Object = value
        If value Is Nothing Then
            ret = DBNull.Value
        End If
        Return ret
    End Function

    'Public Function IsIdentity(columnName As String, schemaTable As DataTable) As Boolean
    '    Dim ret As Boolean = False
    '    Dim drows() As DataRow = schemaTable.Select("COLUMNNAME = '" & columnName & "'")

    '    If CBool(drows(0)("ISIDENTITY")) Then
    '        ret = True
    '    End If

    '    Return ret
    'End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
