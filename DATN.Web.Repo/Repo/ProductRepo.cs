using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

            var param = new Dictionary<string, object>()
            {
                { "$productId", id },
            };
            result = (await this.Provider.QueryAsync<ProductInfo>("Proc_ProductInfo",
                param, CommandType.StoredProcedure)).FirstOrDefault();
            if (result != null)
            {
                result.ProductDetails = await this.GetAsync<ProductDetailEntity>("product_id", result.product_id);
                result.Attributes = await this.Provider.QueryAsync<AttributeClient>("Proc_GetProductAttribute", param, CommandType.StoredProcedure);
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

        public async Task<List<ProductClient>> GetProductHome(SearchModel model)
        {
            var param = new Dictionary<string, object>()
            {
                { "$keyword", model.keyword },
                { "$rating", model.rating },
                { "$fromAmount", model.fromAmount },
                { "$toAmount", model.toAmount },
                { "$category", model.category },
                { "$sort", model.sort },
                { "$page", model.page },
                { "$pageSize", model.pageSize },
            };
            var res = await this.Provider.QueryAsync<ProductClient>("Proc_GetProductHome",
                param, CommandType.StoredProcedure);
            return res;
        }

        public async Task<List<ProductClient>> GetProductRelation(Guid id, int mode)
        {
            var param = new Dictionary<string, object>()
            {
                { "$product_id", id },
                { "$orderby_created_date", mode == 1 },
                { "$orderby_sell", mode == 2 },
            };
            var res = await this.Provider.QueryAsync<ProductClient>("Proc_GetProductRelation",
                param, CommandType.StoredProcedure);
            return res;
        }

        public async Task<List<object>> GetRateOption(Guid id)
        {
            var param = new Dictionary<string, object>()
            {
                { "$product_id", id },
            };
            var res = await this.Provider.QueryAsync<object>("Proc_GetRateOption",
                param, CommandType.StoredProcedure);
            return res;
        }

        public async Task<List<object>> GetCommentProduct(Guid id, string filterCode, int pageNumber, int pageSize)
        {
            var param = new Dictionary<string, object>()
            {
                { "$product_id", id },
                { "$filterCode", filterCode },
                { "$pageNumber", pageNumber },
                { "$pageSize", pageSize },
            };
            var res = await this.Provider.QueryAsync<object>("Proc_GetCommentProduct",
                param, CommandType.StoredProcedure);
            return res;
        }
    }
}