Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Reflection
Imports MRFramework.MRCore

Namespace Helpers
    Namespace MRPropertyInfoHelper

        Public Module MRPropertyInfoHelper
            Public Function CreateAccessor(propertyInfo As PropertyInfo) As IMRPropertyAccessor
                Return DirectCast(Activator.CreateInstance(GetType(PropertyWrapper(Of ,)).MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType), propertyInfo), IMRPropertyAccessor)
            End Function
        End Module

    End Namespace

    Friend Class PropertyWrapper(Of T, tvalue)
        Implements IMRPropertyAccessor
        Private _propertyInfo As PropertyInfo

        Private _getMethod As Func(Of T, tvalue)
        Private _setMethod As Action(Of T, tvalue)

        Public Sub New(propertyInfo As PropertyInfo)
            _propertyInfo = propertyInfo

            Dim mGet As MethodInfo = propertyInfo.GetGetMethod(True)
            Dim mSet As MethodInfo = propertyInfo.GetSetMethod(True)


            _getMethod = DirectCast([Delegate].CreateDelegate(GetType(Func(Of T, tvalue)), mGet), Func(Of T, tvalue))
            _setMethod = DirectCast([Delegate].CreateDelegate(GetType(Action(Of T, tvalue)), mSet), Action(Of T, tvalue))
        End Sub

        Private Function IMRPropertyAccessor_GetValue(source As Object) As Object Implements IMRPropertyAccessor.GetValue
            Try
                Return _getMethod(DirectCast(source, T))
            Catch ex As Exception
                If Debugger.IsAttached Then
                    Debugger.Break()
                End If
                Throw
            End Try
        End Function
        Private Sub IMRPropertyAccessor_SetValue(source As Object, value As Object) Implements IMRPropertyAccessor.SetValue
            Try
                If value Is Nothing Then
                    _setMethod(DirectCast(source, T), Nothing)
                Else
                    _setMethod(DirectCast(source, T), DirectCast(value, tvalue))
                End If
            Catch ex As Exception
                If Debugger.IsAttached Then
                    Debugger.Break()
                End If
                Throw
            End Try
        End Sub


        Public ReadOnly Property Name() As String Implements IMRPropertyAccessor.Name
            Get
                Return _propertyInfo.Name
            End Get
        End Property

        Public ReadOnly Property PropertyInfo() As PropertyInfo Implements IMRPropertyAccessor.PropertyInfo
            Get
                Return _propertyInfo
            End Get
        End Property

    End Class

End Namespace
