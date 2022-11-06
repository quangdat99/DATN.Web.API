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
    /// Controller Sản phẩm
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class ProductController : BaseController<ProductEntity>
    {
        /// <summary>
        /// Service  Sản phẩm
        /// </summary>
        IProductService _productService;

        /// <summary>
        /// Repo  Sản phẩm
        /// </summary>
        IProductRepo _productRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="ProductService"></param>
        /// <param name="ProductRepo"></param>
        public ProductController(IProductService productService, IProductRepo productRepo, IServiceProvider serviceProvider) : base(productService, productRepo, serviceProvider)
        {
            _productService = productService;
            _productRepo = productRepo;
        }

        /// <summary>
        /// Lấy Thông tin người dùng
        /// </summary>
        [HttpGet("info/{id}")]
        public async Task<IActionResult> GetProductInfo(Guid id)
        {
            try
            {
                var res = await _productRepo.GetProductInfo(id);
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

    }
}
