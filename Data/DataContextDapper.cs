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

        public bool ExecuteSqlWithParameters(string sql, DynamicParameters parameters)
        {
            IDbConnection dbconnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return (dbconnection.Execute(sql, parameters) > 0);

            //SqlCommand commandWithParams = new SqlCommand(sql);

            //foreach (SqlParameter param in parameters)
            //{
            //    commandWithParams.Parameters.Add(param);
            //}

            //SqlConnection dbconnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            //dbconnection.Open();

            //commandWithParams.Connection = dbconnection;

            //int rowsAffected = commandWithParams.ExecuteNonQuery();

            //dbconnection.Close();

            //return rowsAffected > 0;
        }

        public IEnumerable<T> LoadDataWithParameters<T>(string sql, DynamicParameters parameters)
        {
            IDbConnection dbconnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbconnection.Query<T>(sql, parameters);

        }

        public T LoadDataSingleWithParameters<T>(string sql, DynamicParameters parameters)
        {
            IDbConnection dbconnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbconnection.QuerySingle<T>(sql, parameters);

        }
    }
}
