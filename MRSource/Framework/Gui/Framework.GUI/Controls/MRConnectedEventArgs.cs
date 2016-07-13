namespace Framework.GUI.Controls
{
    public class MRConnectedEventArgs : System.EventArgs
    {
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
