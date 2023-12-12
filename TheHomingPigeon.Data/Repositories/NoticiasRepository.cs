using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHomingPigeon.Data.Interface;
using TheHomingPigeon.Model;

namespace TheHomingPigeon.Data.Repositories
{
    public class NoticiasRepository : INoticiasRepository
    {
        private readonly MySqlConnection _dbConnection;

        public NoticiasRepository(MysqlConfiguration connectionString)
        {
            _dbConnection = new MySqlConnection(connectionString.ConnectionString);
        }

        public async Task<bool> InsertNews(Noticias noticias)
        {
            var sql = @"INSERT INTO noticias (titulo, fecha, cuerpo) VALUES (@titulo, @fecha, @cuerpo )";
            var rowsAffected = await _dbConnection.ExecuteAsync(sql, new { noticias.titulo , noticias.fecha , noticias.cuerpo });

            return rowsAffected > 0;
        }
    }
}
