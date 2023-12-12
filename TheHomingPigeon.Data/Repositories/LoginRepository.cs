using System.Threading.Tasks;
using TheHomingPigeon.Data.Interface;
using TheHomingPigeon.Model;
using MySql.Data.MySqlClient;
using Dapper;



namespace TheHomingPigeon.Data.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MySqlConnection _dbConnection;

        public LoginRepository(MysqlConfiguration connectionString)
        {
            _dbConnection = new MySqlConnection(connectionString.ConnectionString);
        }
        public async Task<Login> ExistUser(string username)
        {
            var sql = @"SELECT * FROM user WHERE user.username = @Username";

            return await _dbConnection.QueryFirstOrDefaultAsync<Login>(sql, new { Username = username });
        }

      
    }
}
