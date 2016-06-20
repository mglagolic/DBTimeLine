Imports MRFramework

Public Class DBCustomizationPersister
    Inherits MRPersisting.MRPersister

    Public Overrides ReadOnly Property DataBaseTableName As String
        Get
            Return "Config.Customization"
        End Get
    End Property
    Public Overrides ReadOnly Property SQL As String
        Get
            Return "SELECT ID, CustomizationKey, Created, Active, Description FROM " & DataBaseTableName
        End Get
    End Property
End Class