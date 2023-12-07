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
            var sql = @"SELECT user.id_user, CONCAT(user.name, ' ', user.lastname) AS full_name, rol.rol FROM tb_user user INNER JOIN tb_rol rol ON user.id_rol = rol.id_rol";

            return await _dbConnection.QueryAsync<UserResponse>(sql, new { });
        }

        public async Task<bool> InsertUser(User user)
        {
            var hasPassword = BCrypt.Net.BCrypt.HashPassword(user.password);

            var sql = @"INSERT INTO tb_user (name, lastname, username, password, id_rol, status_active) VALUES (@name, @lastname, @username, @hasPassword, @id_rol, 1)";

            var rowsAffected = await _dbConnection.ExecuteAsync(sql, new {  user.username, hasPassword});

            return rowsAffected > 0;
        }

        public async Task<bool> ValidateExistUser(string username)
        {
            var sql = @"SELECT * FROM tb_user WHERE username = @Username AND status_active = 1";

            var result = await _dbConnection.QueryFirstOrDefaultAsync<int>(sql, new { Username = username });

            return result > 0;
        }
    }
}
