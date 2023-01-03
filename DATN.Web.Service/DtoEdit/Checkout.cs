using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class Checkout
    {
        public Guid address_id { get; set; }
        public Guid user_id { get; set; }
        public Guid cart_id { get; set; }
        public int method_payment { get; set; }
        public List<ProductCart> listProduct { get; set; } = new List<ProductCart>();
    }
}
