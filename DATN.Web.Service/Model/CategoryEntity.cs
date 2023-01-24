using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng thông tin danh mục sản phẩm
    /// <summary>
    [Table("category")]
    public class CategoryEntity
    {
        /// <summary>
        /// Định danh của danh mục sản phẩm
        /// <summary>
        [Key]
        public Guid category_id { get; set; }
        /// <summary>
        /// Tên danh mục sản phẩm
        /// <summary>
        public string category_name { get; set; }
        /// <summary>
        /// Tên danh mục sản phẩm
        /// <summary>
        public DateTime created_date { get; set; }
        /// <summary>
        /// 
        /// <summary>
        public bool status { get; set; }
    }
}
