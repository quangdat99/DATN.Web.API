using DATN.Web.Service.Constants;

namespace DATN.Web.Service.DtoEdit
{
    public class GetListOrderDTO
    {
        /// <summary>
        /// UserID
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// Order Status
        /// </summary>
        public string orderStatus { get; set; }
    }
}