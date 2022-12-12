using System;
using DATN.Web.Service.Attributes;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng thông tin mã giảm giá
    /// <summary>
    [Table("voucher")]
    public class VoucherEntity
    {
        /// <summary>
        /// Định danh của mã giảm giá
        /// <summary>
        [Key]
        public Guid vocher_id { get; set; }

        /// <summary>
        /// Tên mã giảm giá
        /// <summary>
        public string voucher_code { get; set; }

        /// <summary>
        /// Thông tin mô tả chi tiết về mã giảm giá
        /// <summary>
        public string description { get; set; }

        /// <summary>
        /// Số phần trăm giảm giá
        /// <summary>
        public decimal? discount { get; set; }

        /// <summary>
        /// Số tiền tối đa được giảm sau khi áp dụng mã giảm giá
        /// <summary>
        public decimal? max_amount { get; set; }

        /// <summary>
        /// Số tiền tối thiểu của đơn hàng để được áp dụng mã giảm giá
        /// <summary>
        public decimal? order_min_amount { get; set; }
    }
}