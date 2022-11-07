using System;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
using Microsoft.AspNetCore.Mvc;

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
        public OrderController(IOrderService orderService, IOrderRepo orderRepo, IServiceProvider serviceProvider) : base(orderService, orderRepo, serviceProvider)
        {
            _orderService = orderService;
            _orderRepo = orderRepo;
        }
        
        /// <summary>
        /// Lấy danh sách đơn hàng theo trạng thái của một người dùng
        /// </summary>hàng
        /// <param name="listOrder">userId và trạng thái đơn </param>
        [HttpPost("orderList")]
        public async Task<IActionResult> GetOrderList([FromBody] ListOrder listOrder)
        {
            try
            {
                var res = await _orderService.GetListOrder(listOrder);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", null);
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }
    }
}
