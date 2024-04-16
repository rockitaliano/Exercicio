using Microsoft.Data.SqlClient;
using System.Data;

namespace Questao5.Infrastructure.Database
{
    public class DataContext : IDisposable
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection Connection { get; set; }

        public SqlConnection OpenConn()
        {
            Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            Connection.Open();

            return Connection;
        }

        public void Dispose()
        {
            if (Connection != null)
                if (Connection.State != ConnectionState.Closed)
                    Connection.Close();
        }
    }
}
