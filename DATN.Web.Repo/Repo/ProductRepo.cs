using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DATN.Web.Service.Contexts;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
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
        public async Task<List<ProductClient>> GetProductRelationOrder(string listProductId)
        {
            var param = new Dictionary<string, object>()
            {
                { "$ListProductId", listProductId },
            };
            var res = await this.Provider.QueryAsync<ProductClient>("Proc_GetProductRelationOrder",
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

        public async Task<List<CommentInfo>> GetCommentProduct(Guid id, string filterCode, int pageNumber, int pageSize)
        {
            var param = new Dictionary<string, object>()
            {
                { "$product_id", id },
                { "$filterCode", filterCode },
                { "$pageNumber", pageNumber },
                { "$pageSize", pageSize },
            };
            var res = await this.Provider.QueryAsync<CommentInfo>("Proc_GetCommentProduct",
                param, CommandType.StoredProcedure);

            return res;
        }
        public override async Task<DAResult> GetDataTable<T>(FilterTable filterTable)
        {
            var table = this.GetTableName(typeof(ProductEntity));
            var columnSql = this.ParseColumn(string.Join(",", filterTable.fields));

            var param = new Dictionary<string, object>();
            var where = this.ParseWhere(filterTable.filter, param);

            IDbConnection cnn = null;
            IList result = null;
            int totalRecord = 0;
            try
            {
                cnn = this.Provider.GetOpenConnection();

                var sb = new StringBuilder($"SELECT p.product_id, p.product_code, p.product_name, " +
                    $"SUM(pd.quantity) AS quantity_int, " +
                    $"MIN(pd.sale_price) AS sale_price_int, " +
                    $"FORMAT(SUM(pd.quantity),0) AS quantity, " +
                    $"IF(p.status = 1, 'Đang bán', 'Ngừng bán') AS status_name, p.status, " +
                    $"CONCAT(IF (MIN(pd.sale_price) = 0 OR MIN(pd.sale_price) = MAX(pd.sale_price), '', CONCAT(FORMAT(MIN(pd.sale_price), 0), 'đ - ')) , FORMAT(MAX(pd.sale_price), 0), 'đ') AS sale_price " +
                    $"FROM product p LEFT JOIN product_detail pd ON p.product_id = pd.product_id ");
                var sqlSummary = new StringBuilder($"SELECT COUNT(*) FROM {table}");

                if (!string.IsNullOrWhiteSpace(where))
                {
                    sb.Append($" WHERE {where}");
                    sqlSummary.Append($" WHERE {where}");
                }

                sb.Append($" GROUP BY p.product_id, p.product_code, p.product_name");

                // Sắp xếp
                if (filterTable.sortBy?.Count > 0 && filterTable.sortType?.Count > 0)
                {
                    sb.Append($" ORDER BY ");
                    for (int i = 0; i < filterTable.sortBy.Count; i++)
                    {
                        if (filterTable.sortBy[i] == "quantity")
                        {
                            sb.Append($" quantity_int {filterTable.sortType[i]}");
                        }
                        else if (filterTable.sortBy[i] == "sale_price")
                        {
                            sb.Append($" sale_price_int {filterTable.sortType[i]}");
                        }
                        else
                        {
                            sb.Append($" {filterTable.sortBy[i]} {filterTable.sortType[i]}");
                        }
                        if (i != filterTable.sortBy.Count - 1)
                        {
                            sb.Append(",");
                        }
                    }
                }
                else
                {
                    sb.Append($" ORDER BY p.created_date DESC");
                }

                if (filterTable.page > 0 && filterTable.size > 0)
                {
                    sb.Append($" LIMIT {filterTable.size} OFFSET {(filterTable.page - 1) * filterTable.size}");
                }


                result = await this.Provider.QueryAsync(cnn, sb.ToString(), param);
                totalRecord = await cnn.ExecuteScalarAsync<int>(sqlSummary.ToString(), param);
            }
            finally
            {
                this.Provider.CloseConnection(cnn);
            }

            return new DAResult(200, Resources.getDataSuccess, "", result, totalRecord);
        }

        public async Task<List<AttributeClient>> GetProductAttribute(Guid productId)
        {

            IDbConnection cnn = null;
            try
            {
                var param = new Dictionary<string, object>();
                cnn = this.Provider.GetOpenConnection();

                var sb = new StringBuilder($"SELECT pa.product_attribute_id, pa.attribute_id, pa.product_id, pa.value, pa.created_date, a.attribute_name " +
                "FROM `product_attribute` pa LEFT JOIN `attribute` a ON pa.attribute_id = a.attribute_id " +
                $"WHERE pa.product_id = '{productId}'");

                var result = await this.Provider.QueryAsync<AttributeClient>(cnn, sb.ToString(), param);
                return result;
            }
            finally
            {
                this.Provider.CloseConnection(cnn);
            }
        }

        public async Task<List<CategoryDtoEdit>> GetProductCategory(Guid productId)
        {

            IDbConnection cnn = null;
            try
            {
                var param = new Dictionary<string, object>();
                cnn = this.Provider.GetOpenConnection();

                var sb = new StringBuilder($"SELECT pc.product_category_id, pc.product_id, pc.category_id, c.category_name " +
                "FROM product_category pc LEFT JOIN category c ON pc.category_id = c.category_id " +
                $"WHERE pc.product_id = '{productId}';");

                var result = await this.Provider.QueryAsync<CategoryDtoEdit>(cnn, sb.ToString(), param);
                return result;
            }
            finally
            {
                this.Provider.CloseConnection(cnn);
            }
        }

        public async Task<ProductEntity> GetProductLastest()
        {
            var sql = ("SELECT * FROM product p ORDER BY p.created_date DESC LIMIT 1;");

            var param = new Dictionary<string, object>
            {
            };
            var result = await Provider.QueryAsync<ProductEntity>(sql, param);
            return result?.FirstOrDefault();
        }
        public async Task<List<ProductEntity>> ListProductCompare(Guid id)
        {
            var param = new Dictionary<string, object>()
            {
                { "$ProductId", id },
            };
            var res = await this.Provider.QueryAsync<ProductEntity>("Proc_GetListProductCompare",
                param, CommandType.StoredProcedure);

            return res;
        }

    }
}
