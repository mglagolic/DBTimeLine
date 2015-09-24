Namespace Enums

    Public Module Enums

        Public Enum eInsertDLOResults
            Success = 0
        End Enum
        Public Enum eUpdateDLOResults
            Success = 0
            ConcurrencyViolation = 1
            DLODeleted = 2
        End Enum

        Public Enum eDeleteDLOResults
            Success = 0
            ConcurrencyViolation = 1
            DLODeleted = 2
        End Enum
    End Module
End Namespace
