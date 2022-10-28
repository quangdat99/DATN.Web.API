using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng thông tin giao tiếp danh mục sản phẩm và sản phẩm
    /// <summary>
    [Table("product_category")]
    public class ProductCategoryEntity
    {
        /// <summary>
        /// Định danh thông tin giap tiếp danh mục sản phẩm và sản phẩm
        /// <summary>
        [Key]
        public Guid product_category_id { get; set; }
        /// <summary>
        /// Đinh danh sản phẩm
        /// <summary>
        public Guid product_id { get; set; }
        /// <summary>
        /// Định danh danh mục sản phẩm
        /// <summary>
        public Guid category_id { get; set; }
    }
}
