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
    /// Controller Loại sản phẩm
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class CategoryController : BaseController<CategoryEntity>
    {
        /// <summary>
        /// Service Loại sản phẩm
        /// </summary>
        ICategoryService _categoryService;

        /// <summary>
        /// Repo Loại sản phẩm
        /// </summary>
        ICategoryRepo _categoryRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="CategoryService"></param>
        /// <param name="CategoryRepo"></param>
        public CategoryController(ICategoryService categoryService, ICategoryRepo categoryRepo, IServiceProvider serviceProvider) : base(categoryService, categoryRepo, serviceProvider)
        {
            _categoryService = categoryService;
            _categoryRepo = categoryRepo;
        }


        /// <summary>
        /// Lấy danh sách các loại sản phẩm
        /// </summary>
        [HttpGet("getCategory")]
        public async Task<IActionResult> GetCategory()
        {
            var context = _contextService.Get();
            try
            {
                var res = await _categoryService.GetCategory();
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.addDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", new List<object>());
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, new List<object>());
                return Ok(actionResult);
            }
        }

    }
}
