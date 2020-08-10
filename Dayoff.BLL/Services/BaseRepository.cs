using Dapper;
using Dayoff.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Dayoff.BLL.Services
{
    public class BaseRepository
    {
        private readonly IDBConnectionFactory _connectionFactory;
        public BaseRepository(IDBConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        protected T QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                try
                {
                    return connection.QueryFirstOrDefault<T>(sql, parameters);
                }
                catch (Exception ex)
                {
                    var message = "!Exception caught: MESSAGE: " + ex.Message + "!Exception caught: EXCEPTION: " + ex.StackTrace;
                    throw new Exception("Server Error-");
                }

            }
        }

        protected List<T> Query<T>(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                try
                {
                    return connection.Query<T>(sql, parameters).ToList();
                }
                catch (Exception ex)
                {
                    var message = "!Exception caught: MESSAGE: " + ex.Message + "!Exception caught: EXCEPTION: " + ex.StackTrace;
                    throw new Exception("Server Error-");
                }
            }
        }

        protected int Execute(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                try
                {
                    return connection.Execute(sql, parameters);
                }
                catch (Exception ex)
                {
                    var message = "!Exception caught: MESSAGE: " + ex.Message + "!Exception caught: EXCEPTION: " + ex.StackTrace;
                    throw new Exception("Server Error-");
                }
            }
        }

        // Other Helpers...

        protected IDbConnection CreateConnection()
        {
            //var i = 0;
            try
            {
                //var error = 10 / i;
                IDbConnection conn = _connectionFactory.GetConnection;
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                // Properly initialize your connection here.
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("Server Error-");
            }
        }
    }
}
