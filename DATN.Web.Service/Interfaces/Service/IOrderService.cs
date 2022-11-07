using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;

namespace DATN.Web.Service.Interfaces.Service
{
    /// <summary>
    /// Interface service đơn hàng
    /// </summary>
    public interface IOrderService : IBaseService
    {
        /// <summary>
        /// Get List Orders
        /// </summary>
        /// <param name="listOrder"></param>
        Task<List<OrderEntity>> GetListOrder(ListOrder listOrder);
    }
}
