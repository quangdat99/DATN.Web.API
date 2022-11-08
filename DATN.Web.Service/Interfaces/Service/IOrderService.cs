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
        Task<List<OrderEntity>> GetListOrder(ListOrder listOrder);

        /// <summary>
        /// Hủy đơn hàng trong trạng thái chờ lấy hàng
        /// </summary>
        /// <param name="cancelOrder"></param>
        Task<OrderEntity> CancelOrder(CancelOrder cancelOrder);
    }
}