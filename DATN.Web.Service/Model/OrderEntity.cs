using System;
using DATN.Web.Service.Attributes;
using DATN.Web.Service.Constants;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng thông tin đơn hàng
    /// <summary>
    [Table("order")]
    public class OrderEntity
    {
        /// <summary>
        /// Định danh thông tin đơn hàng
        /// <summary>
        [Key]
        public Guid order_id { get; set; }

        /// <summary>
        /// Định danh của người dùng
        /// <summary>
        public Guid user_id { get; set; }

        /// <summary>
        /// Tổng số tiền của đơn hàng
        /// <summary>
        public decimal total_amount { get; set; }

        /// <summary>
        /// Trạng thái của đươn hàng 
        /// (5: chờ xác nhận, 2: chờ lấy hàng, 3: đang giao, 
        /// 1: giao thành công, 4: đã hủy đơn, 6: Giao thất bại/trả hàng)
        /// <summary>
        public OrderStatus status { get; set; }

        /// <summary>
        /// Tên người nhận hàng
        /// <summary>
        public string user_name { get; set; }

        /// <summary>
        /// Ngày tạo đơn hàng
        /// <summary>
        public DateTime created_date { get; set; }
        /// <summary>
        /// Ngày Xác nhận đơn hàng
        /// <summary>
        public DateTime? confirm_date { get; set; }
        /// <summary>
        /// Ngày bắt đầu giao hàng
        /// <summary>
        public DateTime? statrt_delivery_date { get; set; }
        /// <summary>
        /// Ngày hủy đơn hàng
        /// <summary>
        public DateTime? cancel_date { get; set; }
        /// <summary>
        /// Ngày giao hàng
        /// <summary>
        public DateTime? delivery_date { get; set; }
        /// <summary>
        /// Ngày giao hàng thành công
        /// <summary>
        public DateTime? success_date { get; set; }
        /// <summary>
        /// Ngày giao hàng thất bại
        /// <summary>
        public DateTime? delivery_failed_date { get; set; }
        /// <summary>
        /// Ngày hoàn hàng trở lại cửa hàng
        /// <summary>
        public DateTime? refund_date { get; set; }
        /// <summary>
        /// Số điện thoại nhận hàng
        /// <summary>
        public string phone { get; set; }
        /// <summary>
        /// Địa chỉ nhận hàng
        /// <summary>
        public string address { get; set; }
        /// <summary>
        /// Giá tiền được  giảm sau khi áp dụng mã giảm giá
        /// <summary>
        public decimal? voucher_amount { get; set; }
        /// <summary>
        /// Tổng số tiền của sản phẩm
        /// <summary>
        public decimal product_amount { get; set; }
        /// <summary>
        /// Mã đơn hàng
        /// <summary>
        public string? order_code { get; set; }
        /// <summary>
        /// Đơn hàng đã được đánh gái chưa
        /// <summary>
        public bool? is_comment { get; set; }
    }
}