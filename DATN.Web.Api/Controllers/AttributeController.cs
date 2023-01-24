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
    /// Controller Thuộc tính sp
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class AttributeController : BaseController<AttributeEntity>
    {
        /// <summary>
        /// Service Thuộc tính sp
        /// </summary>
        IAttributeService _attributeService;

        /// <summary>
        /// Repo Thuộc tính sp
        /// </summary>
        IAttributeRepo _attributeRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="AttributeService"></param>
        /// <param name="AttributeRepo"></param>
        public AttributeController(IAttributeService attributeService, IAttributeRepo attributeRepo, IServiceProvider serviceProvider) : base(attributeService, attributeRepo, serviceProvider)
        {
            _attributeService = attributeService;
            _attributeRepo = attributeRepo;
        }

        /// <summary>
        /// Lấy dữ liệu thêm mới
        /// </summary>
        [HttpGet("newCode")]
        public async Task<IActionResult> NewCode()
        {
            try
            {
                AttributeEntity result = new AttributeEntity();
                result.attribute_id = Guid.NewGuid();
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
        public override async Task<IActionResult> SaveData([FromBody] AttributeEntity model, int mode)
        {
            try
            {
                var result = await _attributeService.SaveData(model, mode);
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
