Imports MRFramework.MRPersisting.Core

Public Class MRUpdateDLOReturnValue
    Implements IMRUpdateDLOReturnValue

    Public Property Result As Enums.eUpdateDLOResults Implements IMRUpdateDLOReturnValue.Result
    Public Property ConcurrentDLO As IMRDLO Implements IMRUpdateDLOReturnValue.ConcurrentDLO

End Class
