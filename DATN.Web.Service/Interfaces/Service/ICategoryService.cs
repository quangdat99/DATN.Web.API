using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Interfaces.Service
{
    /// <summary>
    /// Interface service Loại sản phẩm
    /// </summary>
    public interface ICategoryService : IBaseService
    {

        /// <summary>
        /// Lấy danh sách loại sp
        /// </summary>
        Task<List<CategoryDto>> GetCategory();
        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        Task<CategoryEntity> SaveData(CategoryEntity model, int mode);
    }
}
