using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Interfaces.Service
{
    /// <summary>
    /// Interface service Kích cỡ
    /// </summary>
    public interface ISizeService : IBaseService
    {
        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        Task<SizeEntity> SaveData(SizeEntity model, int mode);
    }
}
