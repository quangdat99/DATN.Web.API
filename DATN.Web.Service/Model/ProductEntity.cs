using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng thông tin sản phẩm
    /// <summary>
    [Table("product")]
    public class ProductEntity
    {
        /// <summary>
        /// Định danh của sản phẩm
        /// <summary>
        [Key]
        public Guid product_id { get; set; }
        /// <summary>
        /// Mã sản phẩm
        /// <summary>
        public string? product_code { get; set; }
        /// <summary>
        /// Tên sản phẩm
        /// <summary>
        public string product_name { get; set; }
        /// <summary>
        /// Tổng quan về sản phẩm
        /// <summary>
        public string? summary { get; set; }
        /// <summary>
        /// Mô tả chi tiết về sản phẩm
        /// <summary>
        public string? description { get; set; }
        /// <summary>
        /// Ngày tạo sản phẩm
        /// <summary>
        public DateTime created_date { get; set; }
        /// <summary>
        /// Trạng thái của sản phẩm (Đang bán/ Ngừng bán)
        /// <summary>
        public int status { get; set; }
    }
}
