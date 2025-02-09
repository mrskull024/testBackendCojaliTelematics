using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infraestructure.Data
{
    public class DbFactory(IConfiguration configuration)
    {
        private readonly string _connectionString = configuration.GetConnectionString("DbCn")!;

        public MySqlConnection Create()
        {
            return new MySqlConnection(_connectionString);
        }
    }

    public static class Procedures
    {
        public static string GetUsers => "Sp_GetUsers";
        public static string GetUserByEmail => "Sp_GetUserByEmail";
        public static string AddUser => "Sp_AddUser";
    }
}
