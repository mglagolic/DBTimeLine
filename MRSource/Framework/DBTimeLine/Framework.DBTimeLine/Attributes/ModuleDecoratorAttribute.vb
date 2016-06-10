<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False, Inherited:=False)>
Public Class ModuleDecoratorAttribute
    Inherits Attribute

    Public Property ClassName As String
    Public Property AssemblyName As String

    Public Sub New(className As String, assemblyName As String)
        Me.ClassName = className
        Me.AssemblyName = assemblyName
    End Sub
End Class
