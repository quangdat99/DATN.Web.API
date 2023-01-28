using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng thông tin chi tiết sản phẩm
    /// <summary>
    [Table("product_detail")]
    public class ProductDetailEntity
    {
        /// <summary>
        /// Định danh thông tin chi tiết sản phẩm
        /// <summary>
        [Key]
        public Guid product_detail_id { get; set; }

        /// <summary>
        /// Định danh của sản phẩm
        /// <summary>
        public Guid product_id { get; set; }

        /// <summary>
        /// Đường dẫn ảnh sản phẩm
        /// <summary>
        public string img_url { get; set; }

        /// <summary>
        /// Giá bán của sản phẩm
        /// <summary>
        public decimal sale_price { get; set; }

        /// <summary>
        /// Giá nhập của sản phẩm
        /// <summary>
        public decimal purchase_price { get; set; }

        /// <summary>
        /// Kích cỡ của sản phẩm
        /// <summary>
        public string size_name { get; set; }

        /// <summary>
        ///  Màu sắc của sản phẩm
        /// <summary>
        public string color_name { get; set; }

        /// <summary>
        /// Số lượng hiện tại của sản phẩm
        /// <summary>
        public int quantity { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime created_date { get; set; }
        /// <summary>
        /// Giảm giáo bao nhiêu %
        /// </summary>
        public decimal? product_discount { get; set; }
        /// <summary>
        /// Giá cũ
        /// </summary>
        public decimal? sale_price_old { get; set; }
    }
}