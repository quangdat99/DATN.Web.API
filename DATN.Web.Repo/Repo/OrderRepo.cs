using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATN.Web.Service.Constants;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Model;
using Microsoft.Extensions.Configuration;

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
        public async Task<List<OrderEntity>> GetListOrder(ListOrder listOrder)
        {
            var sql = string.Format(@"SELECT * FROM {0}
         WHERE user_id=@user_id AND status=@status",
                GetTableName(typeof(OrderEntity)));


            var param = new Dictionary<string, object>
            {
                { "user_id", listOrder.user_id },
                {
                    "status", listOrder.order_status
                },
            };

            OrderStatus orderStatus;
            if (!Enum.IsDefined(typeof(OrderStatus), listOrder.order_status))
            {
                throw new ValidateException("Invalid status", "");
            }

            var result = await Provider.QueryAsync<OrderEntity>(sql, param);
            return result?.ToList();
        }

        /// <summary>
        /// Lấy đơn hàng theo Id
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public async Task<OrderEntity> GetOrderById(string order_id)
        {
            var res = await this.GetAsync<OrderEntity>("order_id", order_id);
            return res.FirstOrDefault();
        }
    }
}