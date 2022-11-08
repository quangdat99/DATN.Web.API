using System.Collections.Generic;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Model;

namespace DATN.Web.Service.Interfaces.Repo
{
    /// <summary>
    /// Interface Repo đơn hàng
    /// </summary>
    public interface IOrderRepo : IBaseRepo
    {
        /// <summary>
        /// Lấy danh sách đơn hàng theo trạng thái của khách hàng
        /// </summary>
        /// <param name="listOrder"></param>
        /// <returns></returns>
        Task<List<OrderEntity>> GetListOrder(ListOrder listOrder);

        /// <summary>
        /// Hủy đơn hàng trong trạng thái chờ lấy hàng
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        Task<OrderEntity> GetOrderById(string order_id);
    }
}