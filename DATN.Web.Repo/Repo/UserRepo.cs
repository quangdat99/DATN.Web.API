using DATN.Web.Service.Interfaces.Repo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DATN.Web.Service.Model;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Properties;
using Dapper;
using System.Collections;

namespace DATN.Web.Repo.Repo
{
    /// <summary>
    /// Repository người dùng
    /// </summary>
    public class UserRepo : BaseRepo, IUserRepo
    {
        IConfiguration _configuration;

        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="configuration">Config của project</param>
        public UserRepo(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<UserInfo> GetUserInfo(Guid id)
        {
            var res = await this.Provider.QueryAsync<UserInfo>("Proc_GetUserInfo",
                new Dictionary<string, object> { { "$UserId", id } }, CommandType.StoredProcedure);
            return res?.FirstOrDefault();
        }

        public async Task<List<AddressEntity>> GetAddressByUserId(Guid userId)
        {
            var res = await this.GetAsync<AddressEntity>("user_id", userId);
            return res;
        }

        public async Task<UserEntity> Login(LoginModel model)
        {
            var sql = string.Format(@"SELECT * FROM {0} 
                    WHERE (email=@email OR phone=@phone)
                    LIMIT 1;",
                this.GetTableName(typeof(UserEntity)));
            var param = new Dictionary<string, object>
            {
                { "email", model.account },
                { "phone", model.account },
                // {"password", model.password }
            };
            var result = await this.Provider.QueryAsync<UserEntity>(sql, param);
            var res = result?.FirstOrDefault();
            if (res == null)
            {
                throw new ValidateException("Tài khoản không tồn tại, vui lòng kiểm tra lại", model, int.Parse(ResultCode.WrongAccount));
            }


            var verified = BCrypt.Net.BCrypt.Verify(model.password, res.password);
            if (!verified)
            {
                throw new ValidateException("Mật khẩu không chính xác, vui lòng kiểm tra lại", model, int.Parse(ResultCode.WrongPassword));
            }

            if (res.is_block == true)
            {
                throw new ValidateException("Tài khoản của bạn không có quyền truy cập", model, 209);

            }
            return result.FirstOrDefault();

        }

        public override async Task<DAResult> GetDataTable<T>(FilterTable filterTable)
        {
            var table = this.GetTableName(typeof(UserEntity));
            var columnSql = this.ParseColumn(string.Join(",", filterTable.fields));

            var param = new Dictionary<string, object>();
            var where = this.ParseWhere(filterTable.filter, param);

            IDbConnection cnn = null;
            IList result = null;
            int totalRecord = 0;
            try
            {
                cnn = this.Provider.GetOpenConnection();

                var sb = new StringBuilder($"SELECT user_id, CONCAT(first_name, ' ', last_name) AS user_name, email, phone, " +
                    $" CASE WHEN gender = 1 THEN 'Nam' " +
                    $" WHEN gender = 2 THEN 'Nữ' " +
                    $" ELSE 'Khác' END as gender_name, " +
                    $" IF(is_block = true, 'Đã chặn', 'Đang hoạt động') as active" +
                    $" FROM {table} " +
                    $" WHERE role <> 2");
                var sqlSummary = new StringBuilder($"SELECT COUNT(*) FROM {table} WHERE role <> 2");

                if (!string.IsNullOrWhiteSpace(where))
                {
                    sb.Append($" WHERE {where}");
                    sqlSummary.Append($" WHERE {where}");
                }


                // Sắp xếp
                if (filterTable.sortBy?.Count > 0 && filterTable.sortType?.Count > 0)
                {
                    sb.Append($" ORDER BY ");
                    for (int i = 0; i < filterTable.sortBy.Count; i++)
                    {
                        sb.Append($" {filterTable.sortBy[i]} {filterTable.sortType[i]}");
                        if (i != filterTable.sortBy.Count - 1)
                        {
                            sb.Append(",");
                        }
                    }
                }
                else
                {
                    sb.Append($" ORDER BY user_name DESC");
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

    }
}
