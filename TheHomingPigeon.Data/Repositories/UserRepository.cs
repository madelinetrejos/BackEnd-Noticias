using TheHomingPigeon.Data.Interface;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TheHomingPigeon.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlConnection _dbConnection;

        public UserRepository(MysqlConfiguration connectionString)
        {
            _dbConnection = new MySqlConnection(connectionString.ConnectionString);
        }

        public async Task<IEnumerable<UserResponse>> GetUsers()
        {
            var sql = @"SELECT * FROM user";

            return await _dbConnection.QueryAsync<UserResponse>(sql, new { });
        }

        public async Task<bool> InsertUser(User user)
        {
            var hasPassword = BCrypt.Net.BCrypt.HashPassword(user.password);

            var sql = @"INSERT INTO user (username, email, password) VALUES (@username, @email, @password)";

            var rowsAffected = await _dbConnection.ExecuteAsync(sql, new { user.username, user.email, user.password });

            return rowsAffected > 0;
        }
    }
}
