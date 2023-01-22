using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Repo.Mysql
{
    public interface IDataBaseProvider
    {
        /// <summary>
        /// Lấy chuỗi kết nối dữ liệu
        /// </summary>
        string GetConnectionString();

        /// <summary>
        /// Lấy kết nối dữ liệu
        /// </summary>
        IDbConnection GetConnection();

        /// <summary>
        /// Lấy kết nối và mở luôn
        /// </summary>
        IDbConnection GetOpenConnection();

        /// <summary>
        /// Giải phóng connection
        /// </summary>
        /// <param name="connection">SQL connection</param>
        void CloseConnection(IDbConnection connection);

        /// <summary>
        /// Thực hiện sql trả về danh sách dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<List<T>> QueryAsync<T>(string commandText, Dictionary<string, object> param,
            CommandType commandType = CommandType.Text);


        /// <summary>
        /// Thực hiện sql trả về danh sách dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<List<T>> QueryAsync<T>(IDbConnection cnn, string commandText, Dictionary<string, object> param,
            CommandType commandType = CommandType.Text);

        /// <summary>
        /// Thực hiện sql trả về danh sách dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<IList> QueryAsync(IDbConnection cnn, string commandText, Dictionary<string, object> param,
            CommandType commandType = CommandType.Text);

        /// <summary>
        /// Thực hiện sql trả về dữ liệu cell đầu tiên
        /// </summary>
        /// <param name="commandText">Câu truy vấn</param>
        /// <param name="param">Tham số là object</param>
        Task<object> ExcuteScalarTextAsync(string commandText, object param);


        /// <summary>
        /// Thực hiện sql trả về dữ liệu cell đầu tiên
        /// </summary>
        /// <param name="commandText">Câu truy vấn</param>
        /// <param name="param">Tham số là object</param>
        Task<object> ExcuteScalarTextAsync(IDbConnection cnn, string commandText, object param);

        /// <summary>
        /// Thực hiện sql trả về row effect
        /// </summary>
        /// <param name="commandText">câu truy vấn</param>
        /// <param name="param">Tham số</param>
        /// <returns></returns>
        Task<int> ExecuteNoneQueryTextAsync(string commandText, object param);

        /// <summary>
        /// Thực hiện sql trả về row effect
        /// </summary>
        /// <param name="commandText">câu truy vấn</param>
        /// <param name="param">Tham số</param>
        /// <returns></returns>
        Task<int> ExecuteNoneQueryTextAsync(IDbConnection cnn, string commandText, object param);
    }
}