using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng lưu các sản phẩm kèm theo của các sản phẩm
    /// <summary>
    [Table("product_relation")]
    public class ProductRelationEntity
    {
        /// <summary>
        /// PK
        /// <summary>
        [Key]
        public Guid product_relation_id { get; set; }
        /// <summary>
        /// Định danh của sản phẩm
        /// <summary>
        public Guid product_id { get; set; }
        /// <summary>
        /// Định danh của sản phẩm kèm theo
        /// <summary>
        public Guid relation_id { get; set; }
        /// <summary>
        /// Số lần xuất hiện trong các đơn hàng cùng với sản phẩm chính
        /// <summary>
        public int count_relation { get; set; }
    }
}
