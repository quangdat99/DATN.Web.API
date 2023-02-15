using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Định danh thông tin giao tiếp sản phẩm và đơn hàng
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
        /// Đinh danh loại sản phẩm
        /// <summary>
        public Guid product_id { get; set; }
        /// <summary>
        /// Đinh danh sản phẩm
        /// <summary>
        public Guid product_detail_id { get; set; }
        /// <summary>
        /// 
        /// <summary>
        public decimal product_amount { get; set; }
        /// <summary>
        /// 
        /// <summary>
        public string product_name { get; set; }
        /// <summary>
        /// 
        /// <summary>
        public string color_name { get; set; }
        /// <summary>
        /// 
        /// <summary>
        public string size_name { get; set; }
        /// <summary>
        /// 
        /// <summary>
        public string url_img { get; set; }
        /// <summary>
        /// Số lượng sp
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Giá cũ
        /// </summary>
        public decimal product_amount_old { get; set; }
        /// <summary>
        /// Giá vốn nhập hàng
        /// </summary>
        public decimal purchase_amount { get; set; }
    }
}
