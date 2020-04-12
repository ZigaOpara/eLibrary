using System;
using MySql.Data.MySqlClient;

namespace eLibrary
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection { get; }

        public AppDb(string connectionString)
        {
#if DEBUG
            Connection = new MySqlConnection(connectionString);
#else
            var start = connectionString.IndexOf(":", StringComparison.Ordinal);
            var constr = connectionString.Substring(0, start) + connectionString.Substring(start + 6);
            constr = constr + ";Port=" + connectionString.Substring(start + 1,  5) + ";";
            Connection = new MySqlConnection(constr);
#endif
            
            Connection.Open();
        }

        public void Dispose() => Connection.Dispose();
    }
}