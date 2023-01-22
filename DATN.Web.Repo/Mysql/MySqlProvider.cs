using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;

namespace DATN.Web.Repo.Mysql
{
    /// <summary>
    /// Thao tác với database
    /// </summary>
    public class MySqlProvider : IDataBaseProvider
    {
        private readonly string _connectionString;

        public MySqlProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual void CloseConnection(IDbConnection connection)
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public IDbConnection GetConnection()
        {
            var cnn = new MySqlConnection(_connectionString);
            return cnn;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            var cnn = GetConnection();
            cnn.Open();
            return cnn;
        }

        public async Task<List<T>> QueryAsync<T>(string commandText, Dictionary<string, object> param,
            CommandType commandType = CommandType.Text)
        {
            IDbConnection cnn = null;
            try
            {
                cnn = GetOpenConnection();
                return await this.QueryAsync<T>(cnn, commandText, param, commandType);
            }
            finally
            {
                CloseConnection(cnn);
            }
        }

        public async Task<List<T>> QueryAsync<T>(IDbConnection cnn, string commandText,
            Dictionary<string, object> param, CommandType commandType = CommandType.Text)
        {
            var dynamicParams = this.GetParameters(param);
            var result = await cnn.QueryAsync<T>(commandText, dynamicParams, commandType: commandType);
            return result.AsList();
        }


        public async Task<IList> QueryAsync(IDbConnection cnn, string commandText,
            Dictionary<string, object> param, CommandType commandType = CommandType.Text)
        {
            var dynamicParams = this.GetParameters(param);
            var result = await cnn.QueryAsync(commandText, dynamicParams, commandType: commandType);
            return result.AsList();
        }

        /// <summary>
        /// Tạo tham số query
        /// </summary>
        /// <param name="param">Tham số truyền vào</param>
        /// <returns></returns>
        private DynamicParameters GetParameters(Dictionary<string, object> param)
        {
            var result = new DynamicParameters();
            if (param != null)
            {
                foreach (var item in param)
                {
                    var value = item.Value;
                    result.Add(item.Key, value: value);
                }
            }

            return result;
        }

        public async Task<object> ExcuteScalarTextAsync(string commandText, object param)
        {
            IDbConnection cnn = null;
            try
            {
                cnn = GetOpenConnection();
                return await this.ExcuteScalarTextAsync(cnn, commandText, param);
            }
            finally
            {
                CloseConnection(cnn);
            }
        }

        public async Task<object> ExcuteScalarTextAsync(IDbConnection cnn, string commandText, object param)
        {
            var result = await cnn.ExecuteScalarAsync(commandText, param, commandType: CommandType.Text);
            return result;
        }

        public async Task<int> ExecuteNoneQueryTextAsync(string commandText, object param)
        {
            IDbConnection cnn = null;
            try
            {
                cnn = GetOpenConnection();
                return await this.ExecuteNoneQueryTextAsync(cnn, commandText, param);
            }
            finally
            {
                CloseConnection(cnn);
            }
        }

        public async Task<int> ExecuteNoneQueryTextAsync(IDbConnection cnn, string commandText, object param)
        {
            var result = await cnn.ExecuteAsync(commandText, param, commandType: CommandType.Text);
            return result;
        }
    }
}