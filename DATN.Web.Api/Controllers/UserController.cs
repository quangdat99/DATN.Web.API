using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
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
        /// Service Người dùng
        /// </summary>
        IUserService _userService;

        /// <summary>
        /// Repo Người dùng
        /// </summary>
        IUserRepo _userRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="UserService"></param>
        /// <param name="UserRepo"></param>
        public UserController(IUserService userService, IUserRepo userRepo, IServiceProvider serviceProvider) : base(userService, userRepo, serviceProvider)
        {
            _userService = userService;
            _userRepo = userRepo;
        }

        /// <summary>
        /// Lấy Thông tin người dùng
        /// </summary>
        [HttpGet("info/{id}")]
        public async Task<IActionResult> GetUserInfo(Guid id)
        {
            try
            {
                var res = await _userRepo.GetUserInfo(id);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", new List<UserEntity>());
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, new List<UserEntity>());
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Lấy danh sách địa chỉ của một người dùng
        /// </summary>
        /// <param name="userId">định danh người dùng</param>
        [HttpGet("{userId}/addresses")]
        public async Task<IActionResult> GetAddressByUserId()
        {
            var context = _contextService.Get();
            try
             {
                var res = await _userRepo.GetAddressByUserId(context.UserId);
                if (res?.Count > 0)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", new List<UserEntity>());
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, new List<UserEntity>());
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var res = await _userService.Login(model);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", null);
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }
    }
}
