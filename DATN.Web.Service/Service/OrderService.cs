using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATN.Web.Service.Constants;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;

namespace DATN.Web.Service.Service
{
    public class OrderService : BaseService, IOrderService
    {
        private IOrderRepo _orderRepo;

        public OrderService(IOrderRepo orderRepo) : base(orderRepo)
        {
            _orderRepo = orderRepo;
        }

        /// <summary>
        /// Get List Orders
        /// </summary>
        /// <param name="listOrder"></param>
        public async Task<List<OrderEntity>> GetListOrder(ListOrder listOrder)
        {
            var existedOrder = await _orderRepo.GetListOrder(listOrder);
            if (existedOrder == null || !existedOrder.Any())
            {
            }

            return existedOrder;
        }

        /// <summary>
        /// Hủy đơn hàng trong trạng thái chờ lấy hàng
        /// </summary>
        /// <param name="cancelOrder"></param>
        public async Task<OrderEntity> CancelOrder(CancelOrder cancelOrder)
        {
            var existedOrder = await _orderRepo.GetOrderById(cancelOrder.order_id);

            if (existedOrder == null)
            {
                throw new ValidateException("Order not available", "");
            }

            if (existedOrder.user_id.ToString() != cancelOrder.user_id)
            {
                throw new ValidateException("Unauthorized", "");
            }

            if (existedOrder.status == OrderStatus.Pending)
            {
                existedOrder.status = OrderStatus.Cancelled;
                await _orderRepo.UpdateAsync<OrderEntity>(existedOrder, "status");
            }
            else
            {
                throw new ValidateException("Not in Pending status", "");
            }

            return existedOrder;
        }
    }
}