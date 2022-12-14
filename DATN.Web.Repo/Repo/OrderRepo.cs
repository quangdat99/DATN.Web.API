using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DATN.Web.Service.Constants;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DATN.Web.Repo.Repo
{
    /// <summary>
    /// Repository đơn hàng
    /// </summary>
    public class OrderRepo : BaseRepo, IOrderRepo
    {
        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="configuration">Config của project</param>
        public OrderRepo(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Lấy danh sách đơn hàng theo trạng thái của khách hàng
        /// </summary>
        /// <param name="listOrder"></param>
        /// <returns></returns>
        public async Task<List<OrderDto>> GetListOrder(ListOrder listOrder)
        {
            var orders = new List<OrderDto>();

            var sql = string.Format(@"SELECT * FROM {0}
                WHERE user_id=@user_id",
                GetTableName(typeof(OrderEntity)));
            if (listOrder.order_status != OrderStatus.All)
            {
                sql += " AND status=@status";
            }

            sql += " ORDER BY created_date DESC LIMIT 10";

            var param = new Dictionary<string, object>
            {
                { "user_id", listOrder.user_id },
                {
                    "status", listOrder.order_status
                },
            };

            if (!Enum.IsDefined(typeof(OrderStatus), listOrder.order_status))
            {
                throw new ValidateException("Invalid status", "");
            }
            var result = await Provider.QueryAsync<OrderEntity>(sql, param);
            if (result != null)
            {
                orders = JsonConvert.DeserializeObject<List<OrderDto>>(JsonConvert.SerializeObject(result));
                foreach (var order in orders)
                {
                    var products = await this.GetAsync<ProductOrderEntity>("order_id", order.order_id);
                    if (products != null)
                    {
                        order.Products = products;
                    }
                }
            }
            return orders;
        }

        /// <summary>
        /// Delete List Product Cart
        /// </summary>
        /// <param name="cartId">CartId </param>
        /// <param name="productIds">CartId </param>
        public async Task<List<ProductCartEntity>> DeleteListProductCart(List<Guid> productIds, Guid cartId)
        {
            var sql = (@"DELETE FROM  `product_cart` WHERE cart_id = @cartId AND product_id IN (@ids)");

            var ids = string.Join(",", productIds);
            var param = new Dictionary<string, object>
            {
                { "cartId", cartId },
                { "ids", ids }
            };
            var result = await Provider.QueryAsync<ProductCartEntity>(sql, param);
            return result;
        }

        /// <summary>
        /// Get Voucher User
        /// </summary>
        /// <param name="user_id">CartId </param>
        /// <param name="voucher_id">CartId </param>
        public async Task<VoucherUserEntity> GetVoucherUser(Guid voucher_id, Guid user_id)
        {
            var sql = string.Format(@"SELECT * FROM {0}
         WHERE user_id=@user_id AND voucher_id=@voucher_id",
                GetTableName(typeof(VoucherUserEntity)));


            var param = new Dictionary<string, object>
            {
                { "user_id", user_id },
                {
                    "voucher_id", voucher_id
                },
            };

            var result = await Provider.QueryAsync<VoucherUserEntity>(sql, param);
            return result?.FirstOrDefault();
        }

        public async Task<OrderStatusCount> OrderStatusCount(Guid userIdd)
        {
            var param = new Dictionary<string, object>()
            {
                { "$user_id", userIdd },
            };
            var res = await this.Provider.QueryAsync<OrderStatusCount>("Proc_GetOrderCount",
                param, CommandType.StoredProcedure);
            return res?.FirstOrDefault();
        }
    }
}