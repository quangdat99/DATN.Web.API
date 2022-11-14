using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DATN.Web.Repo.Repo
{
    /// <summary>
    /// Repository sản phẩm
    /// </summary>
    public class ProductRepo : BaseRepo, IProductRepo
    {
        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="configuration">Config của project</param>
        public ProductRepo(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<ProductInfo> GetProductInfo(Guid id)
        {
            var result = new ProductInfo();
            var master = await this.GetByIdAsync<ProductEntity>(id);
            if (master != null)
            {
                result = JsonConvert.DeserializeObject<ProductInfo>(JsonConvert.SerializeObject(master));
                result.ProductDetails = await this.GetAsync<ProductDetailEntity>("product_id", result.product_id);
                result.Attributes = await this.GetAsync<AttributeEntity>("product_id", result.product_id);
            }

            return result;
        }

        /// <summary>
        /// Get List Of Products
        /// </summary>
        /// <param name="productIds">Product Ids</param>
        public async Task<List<ProductEntity>> GetListProductInfo(List<Guid> productIds)
        {
            var sql = (@"SELECT * FROM  `product` WHERE product_id IN (@ids)");

            var ids = string.Join(",", productIds);
            var param = new Dictionary<string, object>
            {
                { "ids", ids },
            };
            var result = await Provider.QueryAsync<ProductEntity>(sql, param);
            return result;
        }
    }
}