using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Model;

namespace DATN.Web.Service.Interfaces.Service
{
    /// <summary>
    /// Interface service đơn hàng
    /// </summary>
    public interface IOrderService : IBaseService
    {
        /// <summary>
        /// Get List Orders
        /// </summary>
        /// <param name="listOrder"></param>
        Task<List<OrderDto>> GetListOrder(ListOrder listOrder);

        /// <summary>
        /// Hủy đơn hàng trong trạng thái chờ lấy hàng
        /// </summary>
        /// <param name="cancelOrder"></param>
        Task<OrderEntity> CancelOrder(Guid id);

        /// <summary>
        /// Order Payment
        /// </summary>
        /// <param name="orderPayment"></param>
        Task<OrderEntity> OrderPayment(OrderPayment orderPayment, Guid user_id, Guid cart_id);

        /// <summary>
        /// Lấy số lượng trạng thái của đơn hàng
        /// </summary>
        /// <param name="userId"></param>
        Task<OrderStatusCount> OrderStatusCount(Guid userId);
        /// <summary>
        /// thay đổi trạng thái đơn hàng
        /// </summary>
        Task<OrderInfo> ChangeStatus(ChangeStatus changeStatus);
        /// <summary>
        /// bình luận dánh giá đơn hàng
        /// </summary>
        Task<bool> CommentProduct(CommentProduct commentProduct);
    }
}
