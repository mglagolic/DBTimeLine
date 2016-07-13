using System;
using System.Drawing;
using System.Windows.Forms;

namespace Framework.GUI.Controls
{
    public partial class MRDatabaseConnect : UserControl
    {
        public MRDatabaseConnect()
        {
            InitializeComponent();
            btnRefreshDB.Visible = false;
        }

        public MRDatabaseConnect(Persisting.Interfaces.IDatabaseConnector dbConnector) : this()
        {
            DBConnector = dbConnector;
        }

        public void SetDefaults(string serverName, string databaseName, string username, string password)
        {
            cbServers.Text = serverName;
            txtDatabase.Text = databaseName;
            if (username == "")
                cbAuthentication.SelectedIndex = 0;
            else
            {
                cbAuthentication.SelectedIndex = 1;
            }
            txtUsername.Text = username;
            txtPassword.Text = password;
        }

        private Persisting.Interfaces.IDatabaseConnector DBConnector { get; set; }
        public void SetDBConnector(Persisting.Interfaces.IDatabaseConnector dbConnector)
        {
            DBConnector = dbConnector;
        }


        private void btnRefreshServer_Click(object sender, EventArgs e)
        {
            DBConnector.LoadServers();
            cbServers.Items.Clear();
            foreach (string server in DBConnector.Servers)
            {
                cbServers.Items.Add(server);
            }
        }

        private void btnRefreshDB_Click(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            var cnnstr = DBConnector.GenerateConnectionString(cbServers.Text, txtDatabase.Text, txtUsername.Text, txtPassword.Text);
            var msg = "";
            var errorMsg = "";
            try
            {
                DBConnector.Connect(cnnstr);
                msg = "Successfully connected to database.";
                lblTestConnect.Text = "Connected";
                lblTestConnect.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                lblTestConnect.Text = "-- FAILED --";
                lblTestConnect.ForeColor = Color.Red;
            }
            OnDatabaseConnected(DBConnector, new MRConnectedEventArgs() { Message = msg, ErrorMessage = errorMsg, Success = (errorMsg.Length == 0 ? true : false) });
        }

        #region Events and event raisers

        public delegate void DatabaseConnectedEventHandler(object sender, MRConnectedEventArgs e);

        public event DatabaseConnectedEventHandler DatabaseConnected;

        protected internal void OnDatabaseConnected(object sender, MRConnectedEventArgs e)

        {
            if (DatabaseConnected != null)
            {
                DatabaseConnected(sender, e);
            }            
        }
        #endregion

        public string ServerName
        {
            get
            {
                return cbServers.Text;
            }
        }

        public string DatabaseName
        {
            get
            {
                return txtDatabase.Text;
            }
        }

        private void cbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Enabled = (cbAuthentication.SelectedIndex != 0);
            txtPassword.Enabled = (cbAuthentication.SelectedIndex != 0);
        }
    }
}
