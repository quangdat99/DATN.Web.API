using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class ProductCart : ProductEntity
    {

        public Guid product_detail_id { get; set; }
        public Guid? product_cart_id { get; set; }
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
        /// Đường dẫn ảnh sản phẩm
        /// <summary>
        public string img_url { get; set; }

        /// <summary>
        /// Giá bán của sản phẩm
        /// <summary>
        public decimal sale_price { get; set; }
        /// <summary>
        /// Giảm giáo bao nhiêu %
        /// </summary>
        public int quantity_max { get; set; }
        /// <summary>
        /// Giá cũ
        /// </summary>
        public decimal? sale_price_old { get; set; }
    }
}
