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
        Task<List<ProductCart>> GetProductCart(Guid cartId);
        /// <summary>
        /// Mua hàng
        /// </summary>
        /// <param name="checkout"></param>
        /// <returns></returns>
        Task<int> Checkout(Checkout checkout);
        /// <summary>
        /// Xóa sp trong giỏ hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteProductCart(Guid id);
        /// <summary>
        /// Thay đổi số lượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductCartEntity> UpdateQuantity(ProductCartDto mdoel);
    }
}
