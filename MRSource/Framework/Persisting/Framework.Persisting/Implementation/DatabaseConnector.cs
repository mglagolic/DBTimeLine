using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Persisting.Interfaces;
using System.Data;
using System.Data.Sql;
using MRFramework.MRPersisting.Factory;
using System.Data.Common;

namespace Framework.Persisting.Implementation
{
    public class DatabaseConnector : IDatabaseConnector
    {
        public string GenerateConnectionString(string serverInstanceName, string databaseName, string username = "", string password = "", int connectionTimeout = 30)
        {
            var ret = "Data Source={0};Initial Catalog={1}{2};Connection Timeout={3}";
            var security = ";Integrated Security=True";
            if (username != "") security = string.Format(";User ID={0};Password={1}", username, password);

            return string.Format(ret, serverInstanceName, databaseName, security, connectionTimeout.ToString());
        }

        public List<string> Databases
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private static List<string> _Servers { get; } = new List<string>();

        public List<string> Servers
        {
            get
            {
                return _Servers;
            }
        }
        

        public void Connect(string connectionString)
        {
            MRC.GetInstance().ConnectionString = connectionString;
            try
            {
                using (DbConnection cnn = MRC.GetConnection())
                {
                    cnn.Open();
                    object test = null;
                    using (IDbCommand cmd = MRC.GetCommand(cnn))
                    {
                        cmd.CommandText = "SELECT Test = 1";
                        test = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot connect to database.\n", ex);
            }
        }

        public void LoadServers()
        {
            _Servers.Clear();
            using (DataTable dt = SqlDataSourceEnumerator.Instance.GetDataSources())
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string serverInstanceName = dr["servername"].ToString();

                    if (dr["instancename"] != null && !string.IsNullOrEmpty(dr["instancename"].ToString()))
                    {
                        serverInstanceName += "\\" + dr["instancename"].ToString();
                    }

                    if (!_Servers.Contains(serverInstanceName))
                    {
                        _Servers.Add(serverInstanceName);
                    }
                }
            }
        }

        public void LoadDatabases(string serverInstanceName)
        {
            throw new NotImplementedException();
        }

    }
}
