using System;
using DATN.Web.Service.Constants;

namespace DATN.Web.Service.DtoEdit
{
    public class ListOrder
    {
        /// <summary>
        /// UserID
        /// </summary>
        public Guid user_id { get; set; }
        /// <summary>
        /// Order Status
        /// </summary>
        public OrderStatus order_status { get; set; }
    }
}