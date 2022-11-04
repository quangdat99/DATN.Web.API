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
    /// Interface Repo Sản phẩm
    /// </summary>
    public interface IProductRepo : IBaseRepo
    {
        /// <summary>
        /// Lấy thông tin chi tiết sản phẩm
        /// </summary>
        /// <param name="id">id sản phẩm</param>
        Task<ProductInfo> GetProductInfo(Guid id);
    }
}
