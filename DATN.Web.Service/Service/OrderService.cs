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

        public async Task<List<OrderEntity>> GetListOrder(GetListOrderDTO getListOrderDto)
        {
            var existedOrder = await _orderRepo.GetListOrder(getListOrderDto);
            if (existedOrder == null || !existedOrder.Any())
            {
            }

            return existedOrder;
        }
    }
}