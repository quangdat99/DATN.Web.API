using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
using Microsoft.AspNetCore.Mvc;

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
        public ProductController(IProductService productService, IProductRepo productRepo,
            IServiceProvider serviceProvider) : base(productService, productRepo, serviceProvider)
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


        /// <summary>
        /// Get List Of Products
        /// </summary>
        /// <param name="cart_id">Cart Id</param>
        [HttpGet("listProduct/{cart_id}")]
        public async Task<IActionResult> GetListProduct(Guid cart_id)
        {
            try
            {
                var res = await _productService.GetListProductFromCart(cart_id);
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
        /// Delete Product Detail
        /// </summary>
        /// <param name="productDetailId">Cart Id</param>
        [HttpDelete("detail/{productDetailId}")]
        public async Task<IActionResult> DeleteProductDetail(Guid productDetailId)
        {
            try
            {
                var res = await _productService.DeleteProductDetail(productDetailId);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.deleteDataSuccess, "", res);
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
        /// Customer Update Product Detail In Cart
        /// </summary>
        /// <param name="productDetailId">Product Detail Id</param>
        /// <param name="customerUpdateProductDetail">Update Info</param>
        [HttpPost("detail/{productDetailId}")]
        public async Task<IActionResult> DeleteProductDetail(Guid productDetailId,
            [FromBody] CustomerUpdateProductDetail customerUpdateProductDetail)
        {
            try
            {
                var res = await _productService.CustomerUpdateProductDetail(productDetailId,
                    customerUpdateProductDetail);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.editDataSuccess, "", res);
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
        /// Add Single Product ToCart
        /// </summary>
        /// <param name="cartId">Cart ID</param>
        /// <param name="newProductId">New Product Id</param>
        [HttpPost("addProduct/{cartId}")]
        public async Task<IActionResult> AddSingleProductToCart(Guid cartId,
            Guid newProductId)
        {
            try
            {
                var res = await _productService.AddSingleProductToCart(cartId,
                    newProductId);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.addDataSuccess, "", res);
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