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
        public UserRepo(IConfiguration configuration): base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<object> GetUserInfo(Guid id)
        {
            var res = await this.Provider.QueryAsync<object>("Proc_GetUserInfo", new Dictionary<string, object> { { "$UserId", id } }, CommandType.StoredProcedure);
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
                    WHERE (email=@email OR phone=@phone) AND password=@password 
                    LIMIT 1;", 
                    this.GetTableName(typeof(UserEntity)));
            var param = new Dictionary<string, object>
            {
                {"email", model.account },
                {"phone", model.account },
                {"password", model.password }
            };
            var result = await this.Provider.QueryAsync<UserEntity>(sql, param);
            return result?.FirstOrDefault();
        }
    }
}
