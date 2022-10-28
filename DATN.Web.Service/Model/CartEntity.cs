using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng thông tin giỏ hàng
    /// <summary>
    [Table("cart")]
    public class CartEntity
    {
        /// <summary>
        /// Định danh của giỏ hàng
        /// <summary>
        [Key]
        public Guid cart_id { get; set; }
        /// <summary>
        /// Định danh của người dùng
        /// <summary>
        public Guid user_id { get; set; }
    }
}
