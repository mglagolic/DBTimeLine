namespace Framework.GUI.Forms
{
    public partial class DatabaseConnectForm : Framework.GUI.Forms.StatusForm
    {
        public DatabaseConnectForm()
        {
            InitializeComponent();
            databaseConnect1.SetDBConnector(new Persisting.Implementation.DatabaseConnector());
        }
        public void SetDefaults(string serverName, string databaseName, string username, string password)
        {
            databaseConnect1.SetDefaults(serverName, databaseName, username, password);
        }

        private void databaseConnect1_DatabaseConnected(object sender, Controls.DBConnectedEventArgs e)
        {
            Connected = e.Success;
        }

        public bool Connected { get; set; } = false;
        public string ServerName
        {
            get
            {
                return databaseConnect1.ServerName;
            }
        }
        public string DatabaseName
        {
            get
            {
                return databaseConnect1.DatabaseName;
            }
        }
    }
}
