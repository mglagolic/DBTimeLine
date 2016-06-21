namespace Customizations.Core.EventArgs
{
    public class CustomizationLoadedEventArgs: System.EventArgs
    {
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public Customizer Customizer { get; set; }
    }
}
