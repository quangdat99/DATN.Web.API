using DATN.Web.Service.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Contexts
{
    public class ContextData
    {
        /// <summary>
        /// Id người dùng
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Email đăng nhập
        /// </summary>
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public Guid CartId { get; set; }
        public DateTime TokenExpired { get; set; }
        public Role Role { get; set; }
    }
}
