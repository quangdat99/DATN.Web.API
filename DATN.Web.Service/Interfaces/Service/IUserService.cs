using System.Collections.Generic;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Model;

namespace DATN.Web.Service.Interfaces.Service
{
    /// <summary>
    /// Interface service Người dùng
    /// </summary>
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="model"></param>
        Task<Dictionary<string, object>> Login(LoginModel model);
        
        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="resetPassword"></param>
        Task<UserEntity> ResetPassword(ResetPassword resetPassword);
    }
}
