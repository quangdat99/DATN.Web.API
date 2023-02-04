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
    /// Interface Repo giỏ hàng
    /// </summary>
    public interface IProductCartRepo : IBaseRepo
    {
        /// <summary>
        /// Lấy danh sách sp trong giỏ hàng
        /// </summary>
        Task<List<ProductCart>> GetProductCart(Guid cartId);
    }
}
