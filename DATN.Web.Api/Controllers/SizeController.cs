using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
using DATN.Web.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN.Web.Api.Controllers
{
    /// <summary>
    /// Controller Kích thước
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class SizeController : BaseController<SizeEntity>
    {
        /// <summary>
        /// Service Kích thước
        /// </summary>
        ISizeService _sizeService;

        /// <summary>
        /// Repo Kích thước
        /// </summary>
        ISizeRepo _sizeRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="SizeService"></param>
        /// <param name="SizeRepo"></param>
        public SizeController(ISizeService sizeService, ISizeRepo sizeRepo, IServiceProvider serviceProvider) : base(sizeService, sizeRepo, serviceProvider)
        {
            _sizeService = sizeService;
            _sizeRepo = sizeRepo;
        }
        /// <summary>
        /// Lấy dữ liệu thêm mới
        /// </summary>
        [HttpGet("newCode")]
        public async Task<IActionResult> NewCode()
        {
            try
            {
                SizeEntity result = new SizeEntity();
                result.size_id = Guid.NewGuid();
                result.status = true;
                return Ok(new DAResult(200, Resources.addDataSuccess, "", result));
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        [HttpPost("saveData/{mode}")]
        public override async Task<IActionResult> SaveData([FromBody] SizeEntity model, int mode)
        {
            try
            {
                var result = await _sizeService.SaveData(model, mode);
                return Ok(new DAResult(200, Resources.addDataSuccess, "", result));
            }
            catch (ValidateException exception)
            {
                var actionResult = new DAResult(exception.resultCode, exception.Message, "", exception.DataErr);
                return Ok(actionResult);
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }

    }
}
