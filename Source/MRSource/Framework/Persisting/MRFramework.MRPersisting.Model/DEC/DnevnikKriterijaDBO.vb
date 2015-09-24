Namespace DEC
    Public Class DnevnikKriterijaDBO
        Inherits MRPersisting.MRPersister

        Public Overrides ReadOnly Property DataBaseTableName As String
            Get
                Return "DEC_DnevnikKriterija"
            End Get
        End Property
        Public Overrides ReadOnly Property SQL As String
            Get
                Return _
<string>SELECT 
    DEC_DnevnikKriterija.*,
    KRITERIJ_NAZIV = DEC_Kriteriji.Naziv
FROM  DEC_DnevnikKriterija 
left outer join DEC_Kriteriji on DEC_DnevnikKriterija.sifra_kriterija = DEC_Kriteriji.sifra
</string>.Value
            End Get
        End Property


    End Class

End Namespace