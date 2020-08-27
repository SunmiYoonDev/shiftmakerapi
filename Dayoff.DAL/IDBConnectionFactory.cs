using MySql.Data.MySqlClient;

namespace Dayoff.DAL
{
    public interface IDBConnectionFactory
    {
        MySqlConnection GetConnection { get; }
        void CloseConnection();
    }
}
