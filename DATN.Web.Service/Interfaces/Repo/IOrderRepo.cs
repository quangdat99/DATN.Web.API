using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;

namespace DATN.Web.Service.Interfaces.Repo
{
    /// <summary>
    /// Interface Repo đơn hàng
    /// </summary>
    public interface IOrderRepo : IBaseRepo
    {
        /// <summary>
        /// GetListOrder
        /// </summary>
        /// <param name="GetListOrderDTO"></param>
        /// <returns></returns>
        Task<List<OrderEntity>> GetListOrder(GetListOrderDTO getListOrderDto);
    }
}
