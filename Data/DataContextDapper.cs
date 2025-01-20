using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DotnetAPI.Data
{
    public class DataContextDapper
    {
        private readonly IConfiguration _config;

        public DataContextDapper(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbconnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbconnection.Query<T>(sql);

        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbconnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbconnection.QuerySingle<T>(sql);

        }

        public Boolean ExecuteSql(string sql)
        {
            IDbConnection dbconnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return (dbconnection.Execute(sql) > 0);
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbconnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbconnection.Execute(sql);
        }
    }
}
