using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApplicationWithDapper.DataFactory
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connection;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = configuration.GetConnectionString("WebApplicationWithDapperContext");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connection);
    }
}
