Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Reflection
Imports System.Reflection.Emit

Namespace Helpers
    Public Module Helpers
        Public Function Construct(typ As Type, ParamArray args() As Object) As Object
            Dim ret As Object = Nothing
            Try
                ret = Activator.CreateInstance(typ, args)
            Catch ex As Exception
                If Debugger.IsAttached Then
                    Debugger.Break()
                End If
                Throw
            End Try
            Return ret
        End Function

        Private Class ItemFactory(Of T As New)
            Public Function GetNewItem() As T
                Return New T()
            End Function
        End Class

        Public Function Construct(Of T As New)() As Object
            Return (New ItemFactory(Of T)).GetNewItem
        End Function


        Public Function DbNullOrNothing(value As Object, defaultValue As Object) As Object
            If value Is Nothing OrElse IsDBNull(value) Then
                Return defaultValue
            Else
                Return value
            End If
        End Function

        Public Class TEst

        End Class

    End Module



End Namespace