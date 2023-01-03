using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Interfaces.Repo
{
    /// <summary>
    /// Interface Repo Loại sản phẩm
    /// </summary>
    public interface ICategoryRepo : IBaseRepo
    {
        /// <summary>
        /// Lấy ds loại sp
        /// </summary>
        /// <returns></returns>
        Task<List<CategoryDto>> GetCategory();
    }
}
