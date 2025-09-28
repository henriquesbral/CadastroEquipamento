using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sql;

namespace CadastroEquipamento.Infrastructure.DataAccess
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new System.Data.SqlClient.SqlConnection(_connectionString);
    }
}
