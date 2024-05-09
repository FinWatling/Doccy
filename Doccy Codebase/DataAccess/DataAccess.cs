using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLibrary
{
    public class DataAccess(IConfiguration config) : IDataAccess
    {
        private readonly IConfiguration _config = config;
        public string ConnectionString { get; set; } = "Default";

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string? connectionString = _config.GetConnectionString(ConnectionString);

            if (connectionString == null)
            {
                throw new Exception("connection string is null");
            }
            else
            {
                using IDbConnection connection = new SqlConnection(connectionString);
                var data = await connection.QueryAsync<T>(sql, parameters);

                return data.ToList();
            }
        }

        public async Task<T?> LoadSingleRowAsync<T, U>(string sql, U parameters)
        {
            string? connectionString = _config.GetConnectionString(ConnectionString);

            if (connectionString == null)
            {
                throw new Exception("Connection string is null.");
            }
            else
            {
                using IDbConnection connection = new SqlConnection(connectionString);
                var data = await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);

                return data;
            }
        }


        public async Task SaveData<T>(string sql, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionString);

            if (connectionString == null)
            {
                throw new Exception("connection string is null");
            }
            else
            {
                using IDbConnection connection = new SqlConnection(connectionString);
                var data = await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
