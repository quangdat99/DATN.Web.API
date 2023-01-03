using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class OrderStatusCount
    {
        public int All { get; set; }
        public int Acceipt { get; set; }
        public int Pending { get; set; }
        public int Delivering { get; set; }
        public int Delivered { get; set; }
        public int Cancelled { get; set; }
        public int Undelivered { get; set; }
    }
}
