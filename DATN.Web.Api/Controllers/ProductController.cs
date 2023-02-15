using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DATN.Web.Service.Contexts;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
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
        /// Lấy danh sách sản phẩm trang home page có tìm kiếm, sắp xếp,...
        /// </summary>
        [HttpPost("homepage")]
        public async Task<IActionResult> GetProductHome(SearchModel model)
        {
            try
            {
                var res = await _productService.GetProductHome(model);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", new List<ProductClient>());
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, new List<ProductClient>());
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Lấy Thông tin sản phẩm
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
        /// <summary>
        /// Lấy Thông tin sản phẩm liên quan
        /// </summary>
        [HttpGet("relation/{id}/{mode}")]
        public async Task<IActionResult> GetProductRelation(Guid id, int mode)
        {
            try
            {
                var res = await _productRepo.GetProductRelation(id, mode);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", new List<ProductClient>());
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, new List<ProductClient>());
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Lấy Thông tin sản phẩm liên quan
        /// </summary>
        [HttpPost("relation")]
        public async Task<IActionResult> GetProductRelationOrder([FromBody] string listProductId)
        {
            try
            {
                var res = await _productRepo.GetProductRelationOrder(listProductId);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", new List<ProductClient>());
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, new List<ProductClient>());
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

        /// <summary>
        /// Lấy Thông tin các option đánh giá của sản phẩm
        /// </summary>
        [HttpGet("rateOption/{id}")]
        public async Task<IActionResult> GetRateOption(Guid id)
        {
            try
            {
                var res = await _productRepo.GetRateOption(id);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
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

        /// <summary>
        /// Lấy danh sách bình luận của sản phẩm
        /// </summary>
        [HttpPost("commentProduct")]
        public async Task<IActionResult> GetCommentProduct(FilterComment filterComment)
        {
            try
            {
                var res = await _productRepo.GetCommentProduct(filterComment.product_id, filterComment.filterCode, filterComment.pageNumber, filterComment.pageSize);
                if (res != null)
                {
                    foreach (var item in res)
                    {
                        item.avatar = Common.GetUrlImage(Request.Host.ToString(), item.avatar);
                        item.img_url = Common.GetUrlImage(Request.Host.ToString(), item.img_url);
                    }
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
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

        /// <summary>
        /// Lấy danh sách bình luận của sản phẩm
        /// </summary>
        [HttpPost("saveProduct/{mode}")]
        public async Task<IActionResult> SaveProduct([FromBody] ProductEdit saveProduct, int mode)
        {
            try
            {
                var res = await _productService.SaveProduct(saveProduct, mode);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", new List<object>());
                    return Ok(actionResult);
                }
            }
            catch (ValidateException exception)
            {
                var actionResult = new DAResult(exception.resultCode, exception.Message, "", exception.DataErr);
                return Ok(actionResult);
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, new List<object>());
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Lấy Thông tin sản phẩm về để sửa
        /// </summary>
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> GetProductEdit(Guid id)
        {
            try
            {
                var res = await _productService.GetProductEdit(id);
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
        /// <summary>
        /// Lấy dữ liệu thêm mới
        /// </summary>
        [HttpGet("newCode")]
        public async Task<IActionResult> NewCode()
        {
            try
            {
                ProductEdit result = new ProductEdit();
                result.product_id = Guid.NewGuid();
                result.description = String.Empty;
                result.outstanding = 1;
                result.status = true;
                result.product_code = await _productService.NewCode();
                return Ok(new DAResult(200, Resources.addDataSuccess, "", result));
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }
        
        /// <summary>
        /// Lấy danh sách sản phẩm để so sánh
        /// </summary>
        [HttpGet("listProductCompare/{id}")]
        public async Task<IActionResult> ListProductCompare(Guid id)
        {
            try
            {
                var res = await _productRepo.ListProductCompare(id);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
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
