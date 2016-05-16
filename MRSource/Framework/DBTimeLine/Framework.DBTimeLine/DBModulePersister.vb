Imports MRFramework

Public Class DBModulePersister
    Inherits MRPersisting.MRPersister

    Public Overrides ReadOnly Property DataBaseTableName As String
        Get
            Return "DBTimeLine.Module"
        End Get
    End Property
    Public Overrides ReadOnly Property SQL As String
        Get
            Return "SELECT ID, ClassName, AssemblyName, DefaultSchemaName, Active FROM " & DataBaseTableName
        End Get
    End Property
End Class