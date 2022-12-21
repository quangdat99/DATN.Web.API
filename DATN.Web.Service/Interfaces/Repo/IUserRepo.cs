using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Interfaces.Repo
{
    /// <summary>
    /// Interface Repo Người dùng
    /// </summary>
    public interface IUserRepo : IBaseRepo
    {
        /// <summary>
        /// Lấy thông tin người dùng
        /// </summary>
        /// <param name="id">id người dùng</param>
        Task<UserInfo> GetUserInfo(Guid id);

        /// <summary>
        /// Lấy danh sách đại chỉ của một người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<AddressEntity>> GetAddressByUserId(Guid userId);

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="model"></param>
        Task<UserEntity> Login(LoginModel model);
    }
}
