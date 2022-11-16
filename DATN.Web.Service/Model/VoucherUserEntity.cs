using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Web.Service.Constants;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng thông tin giao tiếp người dùng và mã giảm giá
    /// <summary>
    [Table("voucher_user")]
    public class VoucherUserEntity
    {
        /// <summary>
        /// Định danh thông tin giao tiếp người dùng và mã giảm giá
        /// <summary>
        [Key]
        public Guid voucher_user_id { get; set; }

        /// <summary>
        /// Định danh của mã giảm giá
        /// <summary>
        public Guid voucher_id { get; set; }

        /// <summary>
        /// Định danh của người dùng
        /// <summary>
        public Guid user_id { get; set; }
        
        /// <summary>
        /// Voucher Status
        /// <summary>
        public VoucherStatus VoucherStatus { get; set; }
    }
}