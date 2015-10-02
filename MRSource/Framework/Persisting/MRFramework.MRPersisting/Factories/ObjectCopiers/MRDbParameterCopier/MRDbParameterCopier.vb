Imports MRFramework.MRCore
Imports MRFramework.MRPersisting.Factory
Imports System.Data.Common


Public Class MRDbParameterCopier(Of T As Class)
    Inherits MRCopier(Of T)

    Public Overrides Function Copy(obj As Object) As Object
        Dim input As MRDbParameterCopierInput = DirectCast(obj, MRDbParameterCopierInput)
        Dim output As New MRDbParameterCopierOutput

        output.Parameter = MRC.GetParameter(input.Parameter.ParameterName, input.Parameter.DbType, Nothing, cmd:=input.Command) ' ovo je najsporije
        With output.Parameter
            .SourceColumn = input.Parameter.SourceColumn
            .Size = input.Parameter.Size
            .Direction = input.Parameter.Direction
            .SourceColumnNullMapping = input.Parameter.SourceColumnNullMapping
            .SourceVersion = input.Parameter.SourceVersion
        End With
        Return output
    End Function
End Class
