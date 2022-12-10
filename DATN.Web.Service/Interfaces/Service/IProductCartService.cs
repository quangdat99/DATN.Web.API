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
    /// Interface service giỏ hàng
    /// </summary>
    public interface IProductCartService : IBaseService
    {
        /// <summary>
        /// Thêm sp vào giỏ hàng
        /// </summary>
        /// <param name="addToCart"></param>
        /// <returns></returns>
        Task<ProductCartEntity> AddToCart(AddToCart addToCart);

        /// <summary>
        /// Lấy danh sách sp trong giỏ hàng
        /// </summary>
        Task<List<object>> GetProductCart(Guid cartId);
    }
}
