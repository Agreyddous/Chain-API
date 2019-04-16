using System;
using System.Data;
using System.Data.SqlClient;
using Chain.Shared;

namespace Chain.Infra.Context.DataContexts
{
    public class ChainContext : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public ChainContext()
        {
            Connection = new SqlConnection(Settings.ConnectionString);

            try
            {
                Connection.Open();
            }
            catch { }
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}