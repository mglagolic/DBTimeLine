Public Interface IMRCommand(Of TInput As Class, TOutput As Class)
    ReadOnly Property CanExecute(ByVal parameter As TInput) As Boolean
    Overloads Function Execute() As TOutput
    Overloads Function Execute(ByVal parameter As TInput) As TOutput
End Interface


Public Interface IMRCommand_NoInput(Of TOutput As Class)
    ReadOnly Property CanExecute() As Boolean
    Function Execute() As TOutput
End Interface

