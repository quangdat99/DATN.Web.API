using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Interfaces.Repo
{
    public interface IBaseRepo
    {
        /// <summary>
        /// Lấy toàn bộ danh sách thực thể T
        /// </summary>
        /// <returns>Danh sách thực thể T</returns>
        Task<List<T>> GetAsync<T>();

        /// <summary>
        /// Lấy thông tin một thực thể T theo id.   
        /// </summary>
        /// <param name="id">id khóa chính.</param>
        /// <returns>Thông tin thực thể T</returns>
        Task<T> GetByIdAsync<T>(object id);


        /// <summary>
        /// Lấy dữ liệu từ bảng theo điều kiện 1 trường cụ thể 
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu trả về</typeparam>
        /// <param name="field">Tên trường</param>
        /// <param name="value">Giá trị</param>
        /// <param name="op">Toán tử</param>
        Task<List<T>> GetAsync<T>(string field, object value, string op = "=");

        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="entity">dữu liệu</param>
        Task<T> InsertAsync<T>(object entity);

        /// <summary>
        /// Sửa dữ liệu
        /// </summary>
        /// <param name="entity">dữ liệu</param>
        /// <param name="fields">Danh sách các trường cập nhật</param>
        /// <returns></returns>
        Task<T> UpdateAsync<T>(object entity, string fields = null);

        /// <summary>
        /// Xóa dữu liệu
        /// </summary>
        Task<bool> DeleteAsync(object entity);

        IConfiguration GetConfiguration();

        /// <summary>
        /// Lấy dữ liệu combobox
        /// </summary>
        Task<IList> GetComboboxPaging(Type type, string colums, string filter, string sort);

        /// <summary>
        /// Lấy dữ liệu bảng
        /// </summary>
        Task<DAResult> GetDataTable<T>(FilterTable filterTable);
    }
}
