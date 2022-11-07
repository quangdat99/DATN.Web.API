using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
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
    }
}