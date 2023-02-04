using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATN.Web.Service.Constants;
using DATN.Web.Service.Contexts;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using Microsoft.AspNetCore.Http;

namespace DATN.Web.Service.Service
{
    public class OrderService : BaseService, IOrderService
    {
        private IOrderRepo _orderRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IOrderRepo orderRepo,
            IHttpContextAccessor httpContextAccessor) : base(orderRepo)
        {
            _orderRepo = orderRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get List Orders
        /// </summary>
        /// <param name="listOrder"></param>
        public async Task<List<OrderDto>> GetListOrder(ListOrder listOrder)
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
        public async Task<OrderEntity> CancelOrder(Guid id)
        {
            var res = await _orderRepo.GetByIdAsync<OrderEntity>(id);


            if (res.status == OrderStatus.Acceipt || res.status == OrderStatus.Pending)
            {
                res.status = OrderStatus.Cancelled;
                await _orderRepo.UpdateAsync<OrderEntity>(res, "status");
                var productOrders = await _orderRepo.GetAsync<ProductOrderEntity>(nameof(ProductOrderEntity.order_id), id);
                foreach (var item in productOrders)
                {
                    var productDetail = await _orderRepo.GetByIdAsync<ProductDetailEntity>(item.product_detail_id);
                    if (productDetail != null)
                    {
                        productDetail.quantity += item.quantity;
                        await _orderRepo.UpdateAsync<ProductDetailEntity>(productDetail, nameof(ProductDetailEntity.quantity));
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Order Payment
        /// </summary>
        /// <param name="orderPayment"></param>
        public async Task<OrderEntity> OrderPayment(OrderPayment orderPayment, Guid user_id, Guid cart_id)
        {
            var existedOrder = await _orderRepo.GetAsync<OrderEntity>("order_id", orderPayment.order_id);
            var res = existedOrder.FirstOrDefault();

            if (res == null)
            {
                throw new ValidateException("Order not available", "");
            }

            if (res.user_id != user_id)
            {
                throw new ValidateException("Unauthorized", "");
            }

            if (res.status != OrderStatus.Pending)
            {
                throw new ValidateException("Order is either paid or cancelled", "");
            }

            res.status = OrderStatus.Pending;
            await _orderRepo.UpdateAsync<OrderEntity>(res, "status");

            var listProductOrder = await _orderRepo.GetAsync<ProductOrderEntity>("order_id", orderPayment.order_id);
            var productIds = new List<Guid>();
            foreach (var p in listProductOrder)
            {
                productIds.Add(p.product_id);
            }

            await _orderRepo.DeleteListProductCart(productIds, cart_id);

            if (orderPayment.voucher_id != Guid.Empty)
            {
                var existedVoucher = await _orderRepo.GetVoucherUser(orderPayment.voucher_id, user_id);

                if (existedVoucher == null)
                {
                    throw new ValidateException("You don't have this voucher", "");
                }

                existedVoucher.VoucherStatus = VoucherStatus.Used;
                await _orderRepo.UpdateAsync<VoucherUserEntity>(existedVoucher);

                await _orderRepo.UpdateAsync<OrderEntity>(res);

            }

            return res;
        }

        public async Task<OrderStatusCount> OrderStatusCount(Guid userId)
        {
            var data = await _orderRepo.OrderStatusCount(userId);
            return data;
        }

        public async Task<OrderInfo> ChangeStatus(ChangeStatus changeStatus)
        {
            var order = await _orderRepo.GetByIdAsync<OrderEntity>(changeStatus.order_id);
            var productOrders = await _orderRepo.GetAsync<ProductOrderEntity>(nameof(ProductOrderEntity.order_id), changeStatus.order_id);
            order.status = (OrderStatus)changeStatus.status;
            switch (order.status)
            {
                case OrderStatus.Pending:
                    order.statrt_delivery_date = DateTime.Now;
                    await _orderRepo.UpdateAsync<OrderEntity>(order, $"{nameof(OrderEntity.status)},{nameof(OrderEntity.statrt_delivery_date)}");
                    break;
                case OrderStatus.Delivering:
                    order.delivery_date = DateTime.Now;
                    await _orderRepo.UpdateAsync<OrderEntity>(order, $"{nameof(OrderEntity.status)},{nameof(OrderEntity.delivery_date)}");
                    break;
                case OrderStatus.Delivered:
                    order.success_date = DateTime.Now;
                    await _orderRepo.UpdateAsync<OrderEntity>(order, $"{nameof(OrderEntity.status)},{nameof(OrderEntity.success_date)}");
                    break;
                case OrderStatus.Undelivered:
                    order.delivery_failed_date = DateTime.Now;
                    await _orderRepo.UpdateAsync<OrderEntity>(order, $"{nameof(OrderEntity.status)},{nameof(OrderEntity.delivery_failed_date)}");
                    break;
                case OrderStatus.Refund:
                    order.refund_date = DateTime.Now;
                    await _orderRepo.UpdateAsync<OrderEntity>(order, $"{nameof(OrderEntity.status)},{nameof(OrderEntity.refund_date)}");
                    foreach (var item in productOrders)
                    {
                        var productDetail = await _orderRepo.GetByIdAsync<ProductDetailEntity>(item.product_detail_id);
                        if (productDetail != null)
                        {
                            productDetail.quantity += item.quantity;
                            await _orderRepo.UpdateAsync<ProductDetailEntity>(productDetail, nameof(ProductDetailEntity.quantity));
                        }
                    }
                    break;
            }
            return await _orderRepo.GetOrderInfo(changeStatus.order_id);
        }

        public async Task<bool> CommentProduct(CommentProduct commentProduct)
        {
            var order = await _orderRepo.GetByIdAsync<OrderEntity>(commentProduct.order_id);
            if (order != null)
            {
                order.is_comment = true;
                await _orderRepo.UpdateAsync<OrderEntity>(order, nameof(OrderEntity.is_comment));
            }
            foreach (var item in commentProduct.commentProducts)
            {
                var product = await _orderRepo.GetByIdAsync<ProductEntity>(item.product_id);
                if (product != null)
                {

                    item.img_url = Common.SaveImage(_httpContextAccessor.HttpContext.Request.Host.Value, item.img_url);
                    await _orderRepo.InsertAsync<CommentEntity>(item);
                }
            }
            return true;
        }
    }
}