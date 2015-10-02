Imports System.Reflection

Public Interface IMRPropertyAccessor

    ReadOnly Property PropertyInfo() As PropertyInfo
    ReadOnly Property Name() As String
    Function GetValue(source As Object) As Object
    Sub SetValue(source As Object, value As Object)

End Interface
