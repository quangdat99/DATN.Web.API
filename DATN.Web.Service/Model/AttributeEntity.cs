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
    [Table("attribute")]
    public class AttributeEntity
    {
        /// <summary>
        /// Đanh danh bảng thông tin thuộc tính sản phẩm
        /// <summary>
        [Key]
        public Guid attribute_id { get; set; }
        /// <summary>
        /// Định danh của sản phẩm
        /// <summary>
        public Guid product_id { get; set; }
        /// <summary>
        /// Tiêu đề thuộc tính
        /// <summary>
        public string attribute_title { get; set; }
        /// <summary>
        /// Giá trị thuộc tính
        /// <summary>
        public string attribute_value { get; set; }
        /// <summary>
        /// Số thứ tự
        /// <summary>
        public int sort_order { get; set; }
    }
}
