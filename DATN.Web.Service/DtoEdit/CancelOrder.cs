using System;

namespace DATN.Web.Service.DtoEdit
{
    public class CancelOrder
    {
        /// <summary>
        /// UserID
        /// </summary>
        public Guid user_id { get; set; }

        /// <summary>
        /// OrderID
        /// </summary>
        public Guid order_id { get; set; }
    }
}