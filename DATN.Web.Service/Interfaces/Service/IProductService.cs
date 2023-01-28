using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Model;

namespace DATN.Web.Service.Interfaces.Service
{
    /// <summary>
    /// Interface service Sản phẩm
    /// </summary>
    public interface IProductService : IBaseService
    {
        /// <summary>
        /// Get List Of Products
        /// </summary>
        /// <param name="cart_id">Cart Id</param>
        Task<List<ProductEntity>> GetListProductFromCart(Guid cart_id);

        /// <summary>
        /// Delete Product Detail
        /// </summary>
        /// <param name="productDetailId">Product Detail</param>
        Task<ProductDetailEntity> DeleteProductDetail(Guid productDetailId);

        /// <summary>
        /// Customer Update Product Detail In Cart
        /// </summary>
        /// <param name="productDetailId">Product Detail Id</param>
        /// <param name="customerUpdateProductDetail">Update Info</param>
        Task<ProductDetailEntity> CustomerUpdateProductDetail(Guid productDetailId,
            CustomerUpdateProductDetail customerUpdateProductDetail);

        /// <summary>
        /// Add Single Product ToCart
        /// </summary>
        /// <param name="cartId">Cart ID</param>
        /// <param name="newProductId">New Product Id</param>
        Task<List<ProductEntity>> AddSingleProductToCart(Guid cartId,
            Guid newProductId);
        /// <summary>
        /// Lấy danh sách sản phẩm trang home page có tìm kiếm, sắp xếp,...
        /// </summary>
        Task<List<ProductClient>> GetProductHome(SearchModel model);
        /// <summary>
        /// Lưu sản phẩm
        /// </summary>
        Task<object> SaveProduct(ProductEdit saveProduct, int mode);
        /// <summary>
        /// Lấy Thông tin sản phẩm về để sửa
        /// </summary>
        Task<ProductEdit> GetProductEdit(Guid id);


        /// <summary>
        /// Lấy Thông tin mã sản phẩm mới
        /// </summary>
        Task<string> NewCode();
    }
}
