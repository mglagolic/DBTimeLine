using System.Collections.Generic;

namespace Framework.Persisting.Interfaces
{
    public interface IDatabaseConnector
    {
        string GenerateConnectionString(string serverInstanceName, string databaseName, string username = "", string password = "", int connectionTimeout = 30);
        void LoadServers();
        void LoadDatabases(string serverInstanceName);
        List<string> Servers { get; }
        List<string> Databases { get; }
        void Connect(string connectionString);
    }
}
