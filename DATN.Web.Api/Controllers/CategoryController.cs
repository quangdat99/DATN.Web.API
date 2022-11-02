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
        public CategoryController(ICategoryService categoryService, ICategoryRepo categoryRepo) : base(categoryService, categoryRepo)
        {
            _categoryService = categoryService;
            _categoryRepo = categoryRepo;
        }
    }
}
