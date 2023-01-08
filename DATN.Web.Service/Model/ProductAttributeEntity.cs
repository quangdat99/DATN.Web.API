using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{

    /// <summary>
    /// Bảng thông tin các thuộc tính của sản phẩm
    /// <summary>
    [Table("product_attribute")]
    public class ProductAttributeEntity
    {
        /// <summary>
        /// Đanh danh bảng thông tin thuộc tính sản phẩm
        /// <summary>
        /// 
        [Key]
        public Guid product_attribute_id { get; set; }
        /// <summary>
        /// Định danh của nhóm thuộc tính
        /// <summary>
        public Guid attribute_id { get; set; }
        /// <summary>
        /// Định danh của sản phẩm
        /// <summary>
        public Guid product_id { get; set; }
        /// <summary>
        /// Giá trị thuộc tính
        /// <summary>
        public string value { get; set; }
        /// <summary>
        /// 
        /// <summary>
        public DateTime? created_date { get; set; }
    }
}