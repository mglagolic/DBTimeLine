using Framework.Persisting;
using MRFramework.MRPersisting.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centrix1
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            MRC.GetInstance().ProviderName = Properties.Settings.Default.Provider;
            PersistingSettings.Instance.SqlGeneratorFactory = new Framework.Persisting.Implementation.SqlGeneratorFactory();

            TempConnect();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            using (var frm = new Framework.GUI.Forms.MRDatabaseConnectForm())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.SetDefaults(Properties.Settings.Default.DefaultServerInstanceName, Properties.Settings.Default.DefaultDatabaseName, "", "");
                frm.ShowDialog();
                if (frm.Connected)
                {
                    lblDatabase.Text = frm.DatabaseName;
                    lblServer.Text = frm.ServerName;
                }
                else
                {
                    lblDatabase.Text = "--";
                    lblServer.Text = "--";
                }
            }
        }

        private void TempConnect()
        {
            var dc = new Framework.Persisting.Implementation.DatabaseConnector();

            string cnnstr = dc.GenerateConnectionString(Properties.Settings.Default.DefaultServerInstanceName, Properties.Settings.Default.DefaultDatabaseName);
            dc.Connect(cnnstr);
            lblServer.Text = Properties.Settings.Default.DefaultServerInstanceName;
            lblDatabase.Text = Properties.Settings.Default.DefaultDatabaseName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var frm = new Form1())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }

        }
    }
}
