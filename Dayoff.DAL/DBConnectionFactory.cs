using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace Dayoff.DAL
{
    public class DBConnectionFactory : IDBConnectionFactory
    {
        private MySqlConnection _connection;
        private readonly IOptions<MySqlConfiguration> _configs;

        public DBConnectionFactory(IOptions<MySqlConfiguration> Configs)
        {
            _configs = Configs;
        }

        public MySqlConnection GetConnection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new MySqlConnection(_configs.Value.DbConnectionString);
                }
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
