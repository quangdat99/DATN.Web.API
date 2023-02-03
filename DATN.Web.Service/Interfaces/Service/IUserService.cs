using System;
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
        /// Đăng ký
        /// </summary>
        /// <param name="model"></param>
        Task<DAResult> Signup(SignupModel model);

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="resetPassword"></param>
        Task ResetPassword(ResetPassword resetPassword);

        /// <summary>
        /// update user info
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        Task<UserInfo> UpdateUser(UpdateUser updateUser);

        /// <summary>
        /// Lấy token
        /// </summary>
        Task<Dictionary<string, object>> GetToken(Guid userId);

        /// <summary>
        /// chập nhật trạng thái
        /// </summary>
        Task<UserEntity> UpdateStatus(bool status, Guid userId);
    }
}
