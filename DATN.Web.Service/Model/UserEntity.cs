using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Thông tin người dùng
    /// </summary>
    [Table("user")]
    public class UserEntity
    {
        /// <summary>
        /// Định danh người dùng
        /// </summary>
        [Key]
        public Guid user_id { get; set; }
        /// <summary>
        /// Họ
        /// </summary>
        public string first_name { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string last_name { get; set; }
        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Mật khẩu đăng nhập
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public int phone { get; set; }
        /// <summary>
        /// Đường dẫn ảnh đại diện
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// Quyền người dùng (0 : Người mua hàng, 1: Quản trị hệ thống)
        /// </summary>
        public int role { get; set; }
        /// <summary>
        /// Có bị chặn hoạt động hay không
        /// </summary>
        public bool is_block { get; set; }
    }
}
