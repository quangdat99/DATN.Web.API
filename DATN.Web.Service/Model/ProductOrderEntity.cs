using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Đinh jdanh thông tin giao tiếp sản phẩm và đơn hàng
    /// <summary>
    [Table("product_order")]
    public class ProductOrderEntity
    {
        /// <summary>
        /// PK
        /// <summary>
        [Key]
        public Guid product_order_id { get; set; }
        /// <summary>
        /// Định danh đơn hàng
        /// <summary>
        public Guid order_id { get; set; }
        /// <summary>
        /// Đinh danh sản phẩm
        /// <summary>
        public Guid product_id { get; set; }
    }
}
