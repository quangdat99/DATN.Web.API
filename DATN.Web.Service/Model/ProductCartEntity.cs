using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng thông tin giao tiếp sản phẩm và giỏ hàng
    /// <summary>
    [Table("product_cart")]
    public class ProductCartEntity
    {
        /// <summary>
        /// Định danh thông tin giao tiếp sản phẩm và giỏ hàng
        /// <summary>
        [Key]
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
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime created_date { get; set; }

        /// <summary>
        /// Định danh loại sp
        /// <summary>
        public Guid product_id { get; set; }
    }
}
