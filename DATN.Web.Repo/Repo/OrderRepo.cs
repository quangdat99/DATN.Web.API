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

        public async Task<List<OrderEntity>> GetListOrder(GetListOrderDTO getListOrderDto)
        {
            var sql = string.Format(@"SELECT * FROM {0}
         WHERE user_id=@user_id AND status=@status",
                GetTableName(typeof(OrderEntity)));


            var param = new Dictionary<string, object>
            {
                { "user_id", getListOrderDto.user_id },
                {
                    "status", getListOrderDto.orderStatus
                },
            };

            OrderStatus orderStatus;
            if (!Enum.IsDefined(typeof(OrderStatus), getListOrderDto.orderStatus))
            {
                throw new ValidateException("Invalid status", "");
            }

            var result = await Provider.QueryAsync<OrderEntity>(sql, param);
            return result?.ToList();
        }
    }
}