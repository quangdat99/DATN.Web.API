using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class AddToCart
    {
        public Guid product_detail_id { get; set; }
        public decimal quantity { get; set; }
        public Guid cart_id { get; set; }
        public Guid product_id { get; set; }
    }
}
