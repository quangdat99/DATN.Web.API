using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    /// <summary>
    /// Cấu hình Token Authen để gọi API
    /// </summary>
    public class JwtTokenConfig
    {
        /// <summary>
        /// Chìa khóa bí mật
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// Số giây hết hạn
        /// </summary>
        public int ExpiredSeconds { get; set; }
    }
}
