using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN.Web.Api.Controllers
{
    /// <summary>
    /// Controller Người dùng
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class UserController : BaseController<UserEntity>
    {
        /// <summary>
        /// Service nhóm hàng hóa
        /// </summary>
        IUserService _userService;

        /// <summary>
        /// Repo nhóm hàng hóa
        /// </summary>
        IUserRepo _userRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="UserService"></param>
        /// <param name="UserRepo"></param>
        public UserController(IUserService userService, IUserRepo userRepo) : base(userService, userRepo)
        {
            _userService = userService;
            _userRepo = userRepo;
        }
    }
}
