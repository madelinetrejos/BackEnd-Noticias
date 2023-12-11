using TheHomingPigeon.Data.Interface;
using Dapper;
using MySql.Data.MySqlClient;
using TheHomingPigeon.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TheHomingPigeon.Data.Interface
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlConnection _dbConnection;

        public UserRepository(MysqlConfiguration connectionString)
        {
            _dbConnection = new MySqlConnection(connectionString.ConnectionString);
        }

        public async Task<IEnumerable<UserResponse>> GetActiveUsers()
        {
            var sql = @"SELECT iduser, username, email FROM user";

            return await _dbConnection.QueryAsync<UserResponse>(sql, new { });
        }

        public async Task<bool> InsertUser(User user)
        {
            var hasPassword = BCrypt.Net.BCrypt.HashPassword(user.password);

            var sql = @"INSERT INTO user (name, lastname, username, password, status_active) VALUES (@name, @lastname, @username, @hasPassword, )";

            var rowsAffected = await _dbConnection.ExecuteAsync(sql, new {  user.username, hasPassword});

            return rowsAffected > 0;
        }

        public async Task<bool> ValidateExistUser(string username)
        {
            var sql = @"SELECT * FROM user WHERE username = @Username AND status_active = 1";

            var result = await _dbConnection.QueryFirstOrDefaultAsync<int>(sql, new { Username = username });

            return result > 0;
        }
    }
}
