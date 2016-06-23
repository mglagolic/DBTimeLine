namespace DBTimeLiners.DBModules.EventArgs
{
    public class ModuleLoadedEventArgs: System.EventArgs
    {
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public Framework.DBTimeLine.IDBModule DBModule { get; set; }
    }
}
