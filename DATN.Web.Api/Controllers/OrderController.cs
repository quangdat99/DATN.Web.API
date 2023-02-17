using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DATN.Web.Repo.Repo;
using DATN.Web.Service.Constants;
using DATN.Web.Service.Contexts;
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
        public OrderController(IOrderService orderService, IOrderRepo orderRepo, IServiceProvider serviceProvider) :
            base(orderService, orderRepo, serviceProvider)
        {
            _orderService = orderService;
            _orderRepo = orderRepo;
        }

        /// <summary>
        /// Lấy danh sách đơn hàng theo trạng thái của một người dùng
        /// </summary>hàng
        /// <param name="listOrder">userId và trạng thái đơn </param>
        [HttpGet("orderList/{orderStatus}")]
        public async Task<IActionResult> GetOrderList(OrderStatus orderStatus)
        {
            var context = _contextService.Get();
            try
            {
                var res = await _orderService.GetListOrder(new ListOrder()
                {
                    order_status = orderStatus,
                    user_id = context.UserId
                });
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

        /// <summary>
        /// Hủy đơn hàng trong trạng thái Chờ xác nhận và Chờ lấy hàng
        /// </summary>hàng
        /// <param name="cancelOrder">userId và trạng thái đơn </param>
        [HttpPost("cancelOrder/{id}")]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            try
            {
                var res = await _orderService.CancelOrder(id);
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
        
        /// <summary>
        /// Order Payment
        /// </summary>
        /// <param name="orderPayment"></param>
        [HttpPost("payOrder/{order_id}")]
        public async Task<IActionResult> CancelOrder([FromBody] OrderPayment orderPayment, Guid order_id)
        {
            var context = _contextService.Get();
            try
            {
                var userInfo = await _orderRepo.GetByIdAsync<UserEntity>(context.UserId);
                var res = await _orderService.OrderPayment(orderPayment, context.UserId, userInfo.cart_id);
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

        /// <summary>
        /// Lấy danh sách đơn hàng theo trạng thái của một người dùng
        /// </summary>hàng
        /// <param name="listOrder">userId và trạng thái đơn </param>
        [HttpGet("orderStatusCount")]
        public async Task<IActionResult> OrderStatusCount()
        {
            var context = _contextService.Get();
            try
            {
                var res = await _orderService.OrderStatusCount(context.UserId);
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


        /// <summary>
        /// Lấy thông tin đơn hàng
        /// </summary>
        [HttpGet("info/{id}")]
        public async Task<IActionResult> GetOrderInfo(Guid id)
        {
            try
            {
                var res = await _orderRepo.GetOrderInfo(id);
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

        /// <summary>
        /// Thay đổi trạng thái đơn hàng
        /// </summary>
        [HttpPost("changeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatus changeStatus)
        {
            try
            {
                var res = await _orderService.ChangeStatus(changeStatus);
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

        /// <summary>
        /// Bình luận đánh giá đơn hàng
        /// </summary>
        [HttpPost("commentProduct")]
        public async Task<IActionResult> CommentProduct(CommentProduct commentProduct)
        {
            try
            {
                var contextData = _contextService.Get();
                foreach (var item in commentProduct.commentProducts)
                {
                    item.user_id = contextData.UserId;
                    item.created_date = DateTime.Now;
                }
                var res = await _orderService.CommentProduct(commentProduct);
                if (res)
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

        /// <summary>
        /// Lấy Thông tin doanh thu theo đơn hàng
        /// </summary>
        [HttpPost("dashboardOrder")]
        public async Task<IActionResult> GetDashboardOrder([FromBody] FilterTable filterTable)
        {
            try
            {
                var res = await _orderRepo.GetDashboardOrder(filterTable);
                return Ok(res);
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, new List<ProductClient>());
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Lấy tổng số tiền Thông tin doanh thu theo đơn hàng
        /// </summary>
        [HttpPost("dashboardOrderTotal")]
        public async Task<IActionResult> GetDashboardOrderTotal([FromBody] FilterTable filter)
        {
            try
            {
                var res = await _orderRepo.GetDashboardOrderTotal(filter.filter);
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

        /// <summary>
        /// Lấy Thông tin doanh thu theo sản phẩm
        /// </summary>
        [HttpPost("dashboardProduct")]
        public async Task<IActionResult> GetDashboardProduct([FromBody] FilterTable filterTable)
        {
            try
            {
                var res = await _orderRepo.GetDashboardProduct(filterTable);
                return Ok(res);
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, new List<ProductClient>());
                return Ok(actionResult);
            }
        }

    }
}
