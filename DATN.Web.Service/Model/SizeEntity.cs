using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng thông tin kích cỡ
    /// <summary>
    [Table("size")]
    public class SizeEntity
    {
        /// <summary>
        /// Định danh kích cỡ
        /// <summary>
        [Key]
        public Guid size_id { get; set; }
        /// <summary>
        /// Tên kích cỡ
        /// <summary>
        public string size_name { get; set; }
        /// <summary>
        /// Trạng thái: 0 - không sử dụng, 1 - đang sử dụng
        /// <summary>
        public bool status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? created_date { get; set; }
    }
}
