using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Interfaces.Service
{
    /// <summary>
    /// Interface service Màu sắc
    /// </summary>
    public interface IColorService : IBaseService
    {
        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        Task<ColorEntity> SaveData(ColorEntity model, int mode);
    }
}
