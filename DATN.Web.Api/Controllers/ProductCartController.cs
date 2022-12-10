using DATN.Web.Service.DtoEdit;
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
    /// Controller giỏ hàng
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class ProductCartController : BaseController<ProductCartEntity>
    {
        /// <summary>
        /// Service giỏ hàng
        /// </summary>
        IProductCartService _ProductCartService;

        /// <summary>
        /// Repo giỏ hàng
        /// </summary>
        IProductCartRepo _ProductCartRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="ProductCartService"></param>
        /// <param name="ProductCartRepo"></param>
        public ProductCartController(IProductCartService ProductCartService, IProductCartRepo ProductCartRepo, IServiceProvider serviceProvider) : base(ProductCartService, ProductCartRepo, serviceProvider)
        {
            _ProductCartService = ProductCartService;
            _ProductCartRepo = ProductCartRepo;
        }

        /// <summary>
        /// Thêm sp vào giỏ hàng
        /// </summary>
        [HttpPost("addToCart")]
        public async Task<IActionResult> AddToCart(AddToCart addToCart)
        {
            var context = _contextService.Get();
            addToCart.cart_id = context.CartId;
            try
            {
                var res = await _ProductCartService.AddToCart(addToCart);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.addDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", new ProductCartEntity());
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, new ProductCartEntity());
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Thêm sp vào giỏ hàng
        /// </summary>
        [HttpGet("products")]
        public async Task<IActionResult> GetProductCart()
        {
            var context = _contextService.Get();
            try
            {
                var res = await _ProductCartService.GetProductCart(context.CartId);
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
