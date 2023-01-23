using DATN.Web.Service.Attributes;
using DATN.Web.Service.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng thông tin màu sắc
    /// <summary>
    [Table("color")]
    public class ColorEntity
    {
        /// <summary>
        /// Định danh màu
        /// <summary>
        [Key]
        public Guid color_id { get; set; }
        /// <summary>
        /// Tên màu
        /// <summary>
        public string color_name { get; set; }
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
