using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN.Web.Api.Controllers
{
    /// <summary>
    /// Controller Đơn hàng
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class OrderController : BaseController<OrderEntity>
    {
        /// <summary>
        /// Service đơn hàng
        /// </summary>
        IOrderService _orderService;

        /// <summary>
        /// Repo đơn hàng
        /// </summary>
        IOrderRepo _orderRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="OrderService"></param>
        /// <param name="OrderRepo"></param>
        public OrderController(IOrderService orderService, IOrderRepo orderRepo) : base(orderService, orderRepo)
        {
            _orderService = orderService;
            _orderRepo = orderRepo;
        }
    }
}
