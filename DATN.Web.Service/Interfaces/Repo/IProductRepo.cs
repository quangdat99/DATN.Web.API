using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Interfaces.Repo
{
    /// <summary>
    /// Interface Repo Sản phẩm
    /// </summary>
    public interface IProductRepo : IBaseRepo
    {
        /// <summary>
        /// Lấy thông tin chi tiết sản phẩm
        /// </summary>
        /// <param name="id">id sản phẩm</param>
        Task<ProductInfo> GetProductInfo(Guid id);

        /// <summary>
        /// Get List Of Products
        /// </summary>
        /// <param name="productIds">Product Ids</param>
        Task<List<ProductEntity>> GetListProductInfo(List<Guid> productIds);

        /// <summary>
        /// Lấy danh sách sản phẩm trang home page có tìm kiếm, sắp xếp,...
        /// </summary>
        Task<List<ProductClient>> GetProductHome(SearchModel model);

        /// <summary>
        /// Lấy danh sách sản phẩm liên quan
        /// </summary>
        Task<List<ProductClient>> GetProductRelation(Guid id, int mode);

        /// <summary>
        /// Lấy danh sách sản phẩm liên quan theo đơn hàng
        /// </summary>
        Task<List<ProductClient>> GetProductRelationOrder(string listProductId);
        
        /// <summary>
        /// Lấy danh sách các option đánh giá của sp
        /// </summary>
        Task<List<object>> GetRateOption(Guid id);

        /// <summary>
        /// Lấy danh sách bình luận của sản phẩm
        /// </summary>
        Task<List<CommentInfo>> GetCommentProduct(Guid id, string filterCode, int pageNumber, int pageSize);

        /// <summary>
        /// Lấy danh sách thuộc tính của sản phẩm
        /// </summary>
        Task<List<AttributeClient>> GetProductAttribute(Guid productId);

        /// <summary>
        /// Lấy danh sách loại sp của sản phẩm
        /// </summary>
        Task<List<CategoryDtoEdit>> GetProductCategory(Guid productId);

        /// <summary>
        /// Lấy Thông tin sản phẩm gần đây nhất
        /// </summary>
        Task<ProductEntity> GetProductLastest();

        /// <summary>
        /// Lấy danh sách sản phẩm để so sánh
        /// </summary>
        Task<List<ProductEntity>> ListProductCompare(Guid id);
    }
}
