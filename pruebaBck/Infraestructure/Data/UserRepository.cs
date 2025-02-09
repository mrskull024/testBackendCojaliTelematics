using Application.Interfaces;
using Core.DTOs;
using Core.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace Infraestructure.Data
{
    public class UserRepository(IConfiguration configuration) : DbFactory(configuration), IUserRepository
    {
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = new List<User>();
            using MySqlConnection cn = Create();

            try
            {
                await cn.OpenAsync();
                using MySqlCommand cmd = new(Procedures.GetUsers, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    users.Add(new User
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        LastName = reader.GetString("LastName"),
                        Email = reader.GetString("Email"),
                        Country = reader.GetString("Country"),
                        Age = reader.GetInt32("Age"),
                        PhotoUrl = reader.GetString("PhotoUrl")
                    });
                }

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> GetUserByEmail(string email)
        {
            using MySqlConnection cn = Create();

            try
            {
                await cn.OpenAsync();
                using MySqlCommand cmd = new(Procedures.GetUserByEmail, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Email", email.Trim());

                var result = new MySqlParameter("p_Exists", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(result);
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

                int exists = (cmd.Parameters["p_Exists"].Value == DBNull.Value) ? 0 : Convert.ToInt32(cmd.Parameters["p_Exists"].Value);

                var retornValue = exists is 1 ? true : false;

                return retornValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddUser(UserAddDTO user)
        {
            using MySqlConnection cn = Create();

            try
            {
                await cn.OpenAsync();
                using MySqlCommand cmd = new(Procedures.AddUser, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Name", user.Name);
                cmd.Parameters.AddWithValue("p_LastName", user.LastName);
                cmd.Parameters.AddWithValue("p_Email", user.Email);
                cmd.Parameters.AddWithValue("p_Country", user.Country);
                cmd.Parameters.AddWithValue("p_Age", user.Age);
                cmd.Parameters.AddWithValue("p_PhotoUrl", user.PhotoUrl);

                int result = await cmd.ExecuteNonQueryAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
