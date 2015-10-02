Namespace Setup
    Public Module Setup
        Public Sub SetupIntances( _
                                connectionString As String, _
                                providerName As String _
                                )

            With MRPersisting.Factory.MRC.GetInstance
                .ConnectionString = connectionString
                .ProviderName = providerName
            End With
        End Sub
    End Module

End Namespace