using System;
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
            var existedOrder = await _orderRepo.GetAsync<OrderEntity>("order_id", cancelOrder.order_id);
            var res = existedOrder.FirstOrDefault();

            if (res == null)
            {
                throw new ValidateException("Order not available", "");
            }

            if (res.user_id != cancelOrder.user_id)
            {
                throw new ValidateException("Unauthorized", "");
            }

            if (res.status == OrderStatus.Pending)
            {
                res.status = OrderStatus.Cancelled;
                await _orderRepo.UpdateAsync<OrderEntity>(existedOrder, "status");
            }
            else
            {
                throw new ValidateException("Not in Pending status", "");
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

                res.voucher_user_id = existedVoucher.voucher_user_id;
                await _orderRepo.UpdateAsync<OrderEntity>(res);

            }

            return res;
        }
    }
}