using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Model;

namespace DATN.Web.Service.Interfaces.Repo
{
    /// <summary>
    /// Interface Repo đơn hàng
    /// </summary>
    public interface IOrderRepo : IBaseRepo
    {
        /// <summary>
        /// Lấy danh sách đơn hàng theo trạng thái của khách hàng
        /// </summary>
        /// <param name="listOrder"></param>
        /// <returns></returns>
        Task<List<OrderDto>> GetListOrder(ListOrder listOrder);

        /// <summary>
        /// Delete List Product Cart
        /// </summary>
        /// <param name="cartId">CartId </param>
        /// <param name="productIds">Product Id </param>
        Task<List<ProductCartEntity>> DeleteListProductCart(List<Guid> productIds, Guid cartId);

        /// <summary>
        /// Get Voucher User
        /// </summary>
        /// <param name="user_id">CartId </param>
        /// <param name="voucher_id">CartId </param>
        Task<VoucherUserEntity> GetVoucherUser(Guid voucher_id, Guid user_id);

        /// <summary>
        /// Lấy số lượng trạng thái đơn hàng
        /// </summary>
        Task<OrderStatusCount> OrderStatusCount(Guid userIdd);
    }
}
