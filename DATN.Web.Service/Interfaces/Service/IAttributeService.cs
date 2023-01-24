using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Interfaces.Service
{
    /// <summary>
    /// Interface service Thuộc tính sp
    /// </summary>
    public interface IAttributeService : IBaseService
    {
        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        Task<AttributeEntity> SaveData(AttributeEntity model, int mode);

    }
}
