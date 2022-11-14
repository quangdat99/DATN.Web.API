using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DATN.Web.Service.Model;

namespace DATN.Web.Service.Interfaces.Service
{
    /// <summary>
    /// Interface service Sản phẩm
    /// </summary>
    public interface IProductService : IBaseService
    {
        /// <summary>
        /// Get List Of Products
        /// </summary>
        /// <param name="cart_id">Cart Id</param>
        Task<List<ProductEntity>> GetListProductFromCart(Guid cart_id);
    }
}