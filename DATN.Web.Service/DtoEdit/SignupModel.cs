using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class SignupModel
    {
        /// <summary>
        /// Họ người dùng
        /// <summary>
        public string first_name { get; set; }

        /// <summary>
        /// Tên người dùng
        /// <summary>
        public string last_name { get; set; }

        /// <summary>
        /// Địa chỉ email của người dùng
        /// <summary>
        public string email { get; set; }

        /// <summary>
        /// Mật khẩu đăng nhập
        /// <summary>
        public string password { get; set; }

        /// <summary>
        /// Số điện thoại người dùng
        /// <summary>
        public string phone { get; set; }

        /// <summary>
        /// Đường dẫn ảnh đại diện
        /// <summary>
        public string avatar { get; set; }
        /// <summary>
        /// Tỉnh/ thành phố
        /// <summary>
        public string province { get; set; }
        /// <summary>
        /// Mã tỉnh / thành phố
        /// <summary>
        public int province_code { get; set; }
        /// <summary>
        /// quận/ huyện
        /// <summary>
        public string district { get; set; }
        /// <summary>
        /// Mã quận huyện
        /// <summary>
        public int district_code { get; set; }
        /// <summary>
        /// xã/ phường
        /// <summary>
        public string commune { get; set; }
        /// <summary>
        /// Mã xã phường
        /// <summary>
        public int commune_code { get; set; }
        /// <summary>
        /// Địa chỉ chi tiết
        /// <summary>
        public string address_detail { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int? gender { get; set; }
    }
}
