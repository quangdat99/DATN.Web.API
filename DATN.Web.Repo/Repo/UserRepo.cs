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
            return result.FirstOrDefault();

        }
    }
}

