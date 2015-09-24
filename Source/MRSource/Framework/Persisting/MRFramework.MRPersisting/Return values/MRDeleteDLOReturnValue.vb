Imports MRFramework.MRPersisting.Core

Public Class MRDeleteDLOReturnValue
    Implements IMRDeleteDLOReturnValue

    Public Property Result As Enums.eDeleteDLOResults Implements IMRDeleteDLOReturnValue.Result
    Public Property ConcurrentDLO As IMRDLO Implements IMRDeleteDLOReturnValue.ConcurrentDLO

End Class
