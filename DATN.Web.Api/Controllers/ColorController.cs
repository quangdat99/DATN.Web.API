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
    /// Controller Màu sắc
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class ColorController : BaseController<ColorEntity>
    {
        /// <summary>
        /// Service Màu sắc
        /// </summary>
        IColorService _colorService;

        /// <summary>
        /// Repo Màu sắc
        /// </summary>
        IColorRepo _colorRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="ColorService"></param>
        /// <param name="ColorRepo"></param>
        public ColorController(IColorService colorService, IColorRepo colorRepo, IServiceProvider serviceProvider) : base(colorService, colorRepo, serviceProvider)
        {
            _colorService = colorService;
            _colorRepo = colorRepo;
        }


        /// <summary>
        /// Lấy dữ liệu thêm mới
        /// </summary>
        [HttpGet("newCode")]
        public async Task<IActionResult> NewCode()
        {
            try
            {
                ColorEntity result = new ColorEntity();
                result.color_id = Guid.NewGuid();
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
        public override async Task<IActionResult> SaveData([FromBody] ColorEntity model, int mode)
        {
            try
            {
                var result = await _colorService.SaveData(model, mode);
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
