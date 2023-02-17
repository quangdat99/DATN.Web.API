using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DATN.Web.Service.Constants;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DATN.Web.Repo.Repo
{
    /// <summary>
    /// Repository đơn hàng
    /// </summary>
    public class OrderRepo : BaseRepo, IOrderRepo
    {
        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="configuration">Config của project</param>
        public OrderRepo(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Lấy danh sách đơn hàng theo trạng thái của khách hàng
        /// </summary>
        /// <param name="listOrder"></param>
        /// <returns></returns>
        public async Task<List<OrderDto>> GetListOrder(ListOrder listOrder)
        {
            var orders = new List<OrderDto>();

            var sql = string.Format(@"SELECT * FROM {0}
                WHERE user_id=@user_id",
                GetTableName(typeof(OrderEntity)));
            if (listOrder.order_status != OrderStatus.All)
            {
                sql += " AND status=@status";
            }

            sql += " ORDER BY created_date DESC LIMIT 10";

            var param = new Dictionary<string, object>
            {
                { "user_id", listOrder.user_id },
                {
                    "status", listOrder.order_status
                },
            };

            if (!Enum.IsDefined(typeof(OrderStatus), listOrder.order_status))
            {
                throw new ValidateException("Invalid status", "");
            }
            var result = await Provider.QueryAsync<OrderEntity>(sql, param);
            if (result != null)
            {
                orders = JsonConvert.DeserializeObject<List<OrderDto>>(JsonConvert.SerializeObject(result));
                foreach (var order in orders)
                {
                    var products = await this.GetAsync<ProductOrderEntity>("order_id", order.order_id);
                    if (products != null)
                    {
                        order.Products = products;
                    }
                }
            }
            return orders;
        }

        /// <summary>
        /// Delete List Product Cart
        /// </summary>
        /// <param name="cartId">CartId </param>
        /// <param name="productIds">CartId </param>
        public async Task<List<ProductCartEntity>> DeleteListProductCart(List<Guid> productIds, Guid cartId)
        {
            var sql = (@"DELETE FROM  `product_cart` WHERE cart_id = @cartId AND product_id IN (@ids)");

            var ids = string.Join(",", productIds);
            var param = new Dictionary<string, object>
            {
                { "cartId", cartId },
                { "ids", ids }
            };
            var result = await Provider.QueryAsync<ProductCartEntity>(sql, param);
            return result;
        }

        /// <summary>
        /// Get Voucher User
        /// </summary>
        /// <param name="user_id">CartId </param>
        /// <param name="voucher_id">CartId </param>
        public async Task<VoucherUserEntity> GetVoucherUser(Guid voucher_id, Guid user_id)
        {
            var sql = string.Format(@"SELECT * FROM {0}
         WHERE user_id=@user_id AND voucher_id=@voucher_id",
                GetTableName(typeof(VoucherUserEntity)));


            var param = new Dictionary<string, object>
            {
                { "user_id", user_id },
                {
                    "voucher_id", voucher_id
                },
            };

            var result = await Provider.QueryAsync<VoucherUserEntity>(sql, param);
            return result?.FirstOrDefault();
        }

        public async Task<OrderStatusCount> OrderStatusCount(Guid userIdd)
        {
            var param = new Dictionary<string, object>()
            {
                { "$user_id", userIdd },
            };
            var res = await this.Provider.QueryAsync<OrderStatusCount>("Proc_GetOrderCount",
                param, CommandType.StoredProcedure);
            return res?.FirstOrDefault();
        }


        public override async Task<DAResult> GetDataTable<T>(FilterTable filterTable)
        {
            var table = this.GetTableName(typeof(OrderEntity));
            var columnSql = this.ParseColumn(string.Join(",", filterTable.fields));

            var param = new Dictionary<string, object>();
            var where = this.ParseWhere(filterTable.filter, param);

            IDbConnection cnn = null;
            IList result = null;
            int totalRecord = 0;
            try
            {
                cnn = this.Provider.GetOpenConnection();

                var sb = new StringBuilder(
                    $"SELECT o.order_id, o.order_code, CONCAT(u.first_name, ' ', u.last_name) AS buyer_name, o.user_name," +
                    " GROUP_CONCAT(CONCAT(CONCAT('- ',po.product_name), IF(po.color_name IS NOT NULL, CONCAT(' (', po.color_name, ')'), ''), IF(po.size_name IS NOT NULL, CONCAT(' (', po.size_name, ')'), ''), ' x', po.quantity) SEPARATOR '; ') as description," +
                    " CASE WHEN status = 5 THEN 'Chờ xác nhận' " +
                    "WHEN status = 2 THEN 'Chờ lấy hàng' " +
                    "WHEN status = 3 THEN 'Đang giao hàng' " +
                    "WHEN status = 4 THEN 'Đã hủy đơn' " +
                    "WHEN status = 1 THEN 'Giao thành công' " +
                    "WHEN status = 6 THEN 'Giao hàng thất bại' " +
                    "WHEN status = 7 THEN 'Đã hoàn trả lại' " +
                    "ELSE '' END " +
                    " AS status_name, FORMAT(total_amount, 0) as totalAmount, o.status," +
                    " DATE_FORMAT(created_date, '%d-%m-%Y') as str_created_date," + // ngày tạo đơn hàng
                    " DATE_FORMAT(cancel_date, '%d-%m-%Y') as str_cancel_date," + // Ngày hủy đơn hàng
                    " DATE_FORMAT(statrt_delivery_date, '%d-%m-%Y') as str_statrt_delivery_date," + // Ngày bắt đầu giao hàng
                    " DATE_FORMAT(delivery_date, '%d-%m-%Y') as str_delivery_date," + // Ngày giao hàng
                    " DATE_FORMAT(success_date, '%d-%m-%Y') as str_success_date," + // Ngày giao hàng thành công
                    " DATE_FORMAT(delivery_failed_date, '%d-%m-%Y') as str_delivery_failed_date," +// Ngày giao hàng thất bại
                    " DATE_FORMAT(refund_date, '%d-%m-%Y') as str_refund_date" + // Ngày hoàn hàng trở lại cửa hàng
                    $" FROM {table} o LEFT JOIN `product_order` po ON o.order_id = po.order_id " +
                    " LEFT JOIN user u ON u.user_id = o.user_id "
                    );
                var sqlSummary = new StringBuilder($"SELECT COUNT(*) FROM {table}");

                if (!string.IsNullOrWhiteSpace(where))
                {
                    sb.Append($" WHERE {where}");
                    sqlSummary.Append($" WHERE {where}");
                }

                sb.Append($" GROUP BY o.order_id, u.first_name, u.last_name, o.order_code, status_name, o.status, str_created_date, str_cancel_date, str_statrt_delivery_date, str_delivery_date," +
                    $" str_success_date, str_delivery_failed_date, str_refund_date ");


                // Sắp xếp
                if (filterTable.sortBy?.Count > 0 && filterTable.sortType?.Count > 0)
                {
                    sb.Append($" ORDER BY ");
                    for (int i = 0; i < filterTable.sortBy.Count; i++)
                    {

                        if (filterTable.sortBy[i] == "totalAmount")
                        {
                            sb.Append($" total_amount {filterTable.sortType[i]}");
                        }
                        else if (filterTable.sortBy[i] == "status_name")
                        {
                            sb.Append($" status {filterTable.sortType[i]}");
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
                    sb.Append($" ORDER BY created_date DESC");
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
        public async Task<TotalResult> GetDashboardOrderTotal(string filter)
        {


            var param = new Dictionary<string, object>();
            var where = this.ParseWhere(filter, param, "o");

            IDbConnection cnn = null;
            TotalResult totalResult;
            try
            {
                cnn = this.Provider.GetOpenConnection();

                var sqlTotal = new StringBuilder(
                    "SELECT " +
                      "FORMAT(SUM(po.product_amount * po.quantity), 0) AS TotalAmount, " +
                      "FORMAT(SUM(IFNULL(po.purchase_amount, 0) * po.quantity), 0) AS PurchaseAmount, " +
                      "FORMAT(SUM(po.product_amount * po.quantity) - SUM(IFNULL(po.purchase_amount, 0) * po.quantity), 0) AS ProfitAmount " +
                    "FROM `order` o " +
                      "LEFT JOIN product_order po " +
                        "ON o.order_id = po.order_id " +
                        "WHERE o.success_date IS NOT NULL "

                    );

                if (!string.IsNullOrWhiteSpace(where))
                {
                    sqlTotal.Append($" AND {where}");
                }
                totalResult = (await cnn.QueryAsync<TotalResult>(sqlTotal.ToString(), param)).ToList().FirstOrDefault();
            }
            finally
            {
                this.Provider.CloseConnection(cnn);
            }
            return totalResult;
        }
        public async Task<DAResult> GetDashboardOrder(FilterTable filterTable)
        {
            var table = this.GetTableName(typeof(OrderEntity));
            var columnSql = this.ParseColumn(string.Join(",", filterTable.fields));

            var param = new Dictionary<string, object>();
            var where = this.ParseWhere(filterTable.filter, param, "o");

            IDbConnection cnn = null;
            IList result = null;
            int totalRecord = 0;
            TotalResult totalResult;
            try
            {
                cnn = this.Provider.GetOpenConnection();

                var sb = new StringBuilder(
                            "SELECT " +
                            "  o.order_id, " +
                            "  o.order_code, " +
                            "  CONCAT(u.first_name, ' ', u.last_name) AS buy_name, " +
                            "  DATE_FORMAT(o.created_date, '%d-%m-%Y') AS created_date, " +
                            "  DATE_FORMAT(o.success_date, '%d-%m-%Y') AS success_date, " +
                            "  GROUP_CONCAT(CONCAT(CONCAT('- ', po.product_name), IF(po.color_name IS NOT NULL, CONCAT(' (', po.color_name, ')'), ''), IF(po.size_name IS NOT NULL, CONCAT(' (', po.size_name, ')'), ''), ' x', po.quantity) SEPARATOR '; ') AS description, " +
                            "  FORMAT(o.total_amount, 0) AS totalAmount, " +
                            "  FORMAT(SUM(IFNULL(po.purchase_amount, 0) * po.quantity), 0) AS purchasePrice, " +
                            "  SUM(IFNULL(po.purchase_amount, 0) * po.quantity) AS purchasePriceInt, " +
                            "  FORMAT(o.total_amount - SUM(IFNULL(po.purchase_amount, 0) * po.quantity), 0) AS profitAmount, " +
                            "  o.total_amount - SUM(IFNULL(po.purchase_amount, 0) * po.quantity) AS profitAmountInt " +
                            "FROM `order` o " +
                            "  LEFT JOIN product_order po " +
                            "    ON o.order_id = po.order_id " +
                            "  LEFT JOIN user u " +
                            "    ON o.user_id = u.user_id " +
                            "WHERE o.success_date IS NOT NULL "
                    );
                var sqlSummary = new StringBuilder($"SELECT COUNT(*) FROM {table} o WHERE success_date IS NOT NULL ");

                if (!string.IsNullOrWhiteSpace(where))
                {
                    sb.Append($" AND {where}");
                    sqlSummary.Append($" AND {where}");
                }

                sb.Append($" GROUP BY o.order_id, u.first_name, u.last_name, o.order_code ");


                // Sắp xếp
                if (filterTable.sortBy?.Count > 0 && filterTable.sortType?.Count > 0)
                {
                    sb.Append($" ORDER BY ");
                    for (int i = 0; i < filterTable.sortBy.Count; i++)
                    {

                        if (filterTable.sortBy[i] == "totalAmount")
                        {
                            sb.Append($" total_amount {filterTable.sortType[i]}");
                        }
                        else if (filterTable.sortBy[i] == "purchasePrice")
                        {
                            sb.Append($" purchasePriceInt {filterTable.sortType[i]}");
                        }
                        else if (filterTable.sortBy[i] == "profitAmount")
                        {
                            sb.Append($" profitAmountInt {filterTable.sortType[i]}");
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
                    sb.Append($" ORDER BY created_date DESC");
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
        public async Task<DAResult> GetDashboardProduct(FilterTable filterTable)
        {
            var table = this.GetTableName(typeof(ProductOrderEntity));
            var columnSql = this.ParseColumn(string.Join(",", filterTable.fields));

            var param = new Dictionary<string, object>();
            var where = this.ParseWhere(filterTable.filter, param, "o");

            IDbConnection cnn = null;
            IList result = null;
            int totalRecord = 0;
            TotalResult totalResult;
            try
            {
                cnn = this.Provider.GetOpenConnection();

                var sb = new StringBuilder(
                            " SELECT " +
                              " CONCAT(po.product_name, IF(po.color_name IS NOT NULL, CONCAT(' (', po.color_name, ')'), ''), IF(po.size_name IS NOT NULL, CONCAT(' (', po.size_name, ')'), '')) AS description, " +
                              " SUM(po.quantity) quantity, " +
                              " FORMAT(SUM(po.product_amount * po.quantity), 0) AS totalAmount, " +
                              " SUM(po.product_amount * po.quantity) AS totalAmountInt, " +
                              " FORMAT(SUM(IFNULL(po.purchase_amount, 0) * po.quantity), 0) AS purchasePrice, " +
                              " SUM(IFNULL(po.purchase_amount, 0) * po.quantity) AS purchasePriceInt, " +
                              " FORMAT(SUM(po.product_amount * po.quantity) - SUM(IFNULL(po.purchase_amount, 0) * po.quantity), 0) AS profitAmount, " +
                              " SUM(po.product_amount * po.quantity) - SUM(IFNULL(po.purchase_amount, 0) * po.quantity) AS profitAmountInt " +
                            " FROM product_order po " +
                              " LEFT JOIN `order` o " +
                                " ON o.order_id = po.order_id " +
                            " WHERE o.success_date IS NOT NULL " 
                    );
                var sqlSummary = new StringBuilder($"SELECT COUNT(*) FROM (SELECT po.product_name, po.color_name, po.size_name FROM {table} po LEFT JOIN `order` o ON o.order_id = po.order_id WHERE success_date IS NOT NULL ");

                if (!string.IsNullOrWhiteSpace(where))
                {
                    sb.Append($" AND {where}");
                    sqlSummary.Append($" AND {where}");
                }

                sb.Append($" GROUP BY po.product_name, po.color_name, po.size_name ");
                sqlSummary.Append($" GROUP BY po.product_name, po.color_name, po.size_name) as t ");


                // Sắp xếp
                if (filterTable.sortBy?.Count > 0 && filterTable.sortType?.Count > 0)
                {
                    sb.Append($" ORDER BY ");
                    for (int i = 0; i < filterTable.sortBy.Count; i++)
                    {

                        if (filterTable.sortBy[i] == "totalAmount")
                        {
                            sb.Append($" totalAmountInt {filterTable.sortType[i]}");
                        }
                        else if (filterTable.sortBy[i] == "purchasePrice")
                        {
                            sb.Append($" purchasePriceInt {filterTable.sortType[i]}");
                        }
                        else if (filterTable.sortBy[i] == "profitAmount")
                        {
                            sb.Append($" profitAmountInt {filterTable.sortType[i]}");
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
                    sb.Append($" ORDER BY product_name ASC");
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

        public async Task<OrderInfo> GetOrderInfo(Guid id)
        {
            var param = new Dictionary<string, object>();
            IDbConnection cnn = null;
            OrderInfo result = null;
            try
            {
                cnn = this.Provider.GetOpenConnection();

                var sb = new StringBuilder(
                    $"SELECT o.order_id, o.order_code, CONCAT(u.first_name, ' ', u.last_name) AS buyer_name, o.user_name, o.phone, o.address," +
                    " CASE WHEN status = 5 THEN 'Chờ xác nhận' " +
                    "WHEN status = 2 THEN 'Chờ lấy hàng' " +
                    "WHEN status = 3 THEN 'Đang giao hàng' " +
                    "WHEN status = 4 THEN 'Đã hủy đơn' " +
                    "WHEN status = 1 THEN 'Giao thành công' " +
                    "WHEN status = 6 THEN 'Giao hàng thất bại' " +
                    "WHEN status = 7 THEN 'Đã hoàn trả lại' " +
                    "ELSE '' END " +
                    " AS status_name, FORMAT(total_amount, 0) as totalAmount, o.status," +
                    " DATE_FORMAT(created_date, '%d-%m-%Y    %H:%i:%s') as str_created_date," + // ngày tạo đơn hàng
                    " DATE_FORMAT(cancel_date, '%d-%m-%Y   %H:%i:%s') as str_cancel_date," + // Ngày hủy đơn hàng
                    " DATE_FORMAT(statrt_delivery_date, '%d-%m-%Y   %H:%i:%s') as str_statrt_delivery_date," + // Ngày xác nhận đơn hàng
                    " DATE_FORMAT(delivery_date, '%d-%m-%Y   %H:%i:%s') as str_delivery_date," + // Ngày bắt đầu giao 
                    " DATE_FORMAT(success_date, '%d-%m-%Y   %H:%i:%s') as str_success_date," + // Ngày giao hàng thành công
                    " DATE_FORMAT(delivery_failed_date, '%d-%m-%Y   %H:%i:%s') as str_delivery_failed_date," +// Ngày giao hàng thất bại
                    " DATE_FORMAT(refund_date, '%d-%m-%Y   %H:%i:%s') as str_refund_date" + // Ngày hoàn hàng trở lại cửa hàng
                    $" FROM `order` o " +
                    " LEFT JOIN user u ON u.user_id = o.user_id" +
                    $" WHERE o.order_id = '{id}'");

                result = (await this.Provider.QueryAsync<OrderInfo>(cnn, sb.ToString(), param))?.FirstOrDefault();
                result.productOrders = await this.GetAsync<ProductOrderEntity>(nameof(ProductOrderEntity.order_id), id);
            }
            finally
            {
                this.Provider.CloseConnection(cnn);
            }

            return result;
        }
    }
}