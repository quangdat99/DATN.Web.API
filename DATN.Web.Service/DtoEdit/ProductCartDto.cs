using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class ProductCartDto : ProductEntity
    {
        public Guid product_cart_id { get; set; }

        /// <summary>
        /// Định danh giỏ hàng
        /// <summary>
        public Guid cart_id { get; set; }

        /// <summary>
        /// Định danh của sản phẩm
        /// <summary>
        public Guid product_detail_id { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal quantity { get; set; }
    }
}
