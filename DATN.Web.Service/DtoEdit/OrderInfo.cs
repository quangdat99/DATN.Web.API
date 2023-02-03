using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class OrderInfo
    {

        public Guid order_id { get; set; }
        public string? order_code { get; set; }
        public Guid user_id { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string buyer_name { get; set; }
        public string user_name { get; set; }
        public string status_name { get; set; }
        public string totalAmount { get; set; }
        public int status { get; set; }
        public string str_created_date { get; set; }
        public string str_cancel_date { get; set; }
        public string str_statrt_delivery_date { get; set; }
        public string str_delivery_date { get; set; }
        public string str_success_date { get; set; }
        public string str_delivery_failed_date { get; set; }
        public string str_refund_date { get; set; }

        public List<ProductOrderEntity> productOrders { get; set; } = new List<ProductOrderEntity>();
    }
}
