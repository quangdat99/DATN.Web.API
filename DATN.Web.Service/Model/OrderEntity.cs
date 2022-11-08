﻿using System;
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
        /// Trạng thái của đươn hàng (0: chờ lấy hàng, 1: đang giao, 2: giao thành công, 3: đã hủy đơn, 4: giao hàng thất bại)
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
        public DateTime? succes_date { get; set; }

        /// <summary>
        /// Số điện thoại nhận hàng
        /// <summary>
        public int phone { get; set; }

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
    }
}