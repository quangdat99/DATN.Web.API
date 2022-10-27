using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Interfaces.Service
{
    public interface IBaseService
    {
        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="entity">dữu liệu</param>
        Task<object> InsertAsync<T>(object entity);


        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity">dữu liệu</param>
        Task<object> UpdateAsync<T>(object entity);
    }
}
