using DATN.Web.Service.Interfaces.Repo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DATN.Web.Service.Model;

namespace DATN.Web.Repo.Repo
{
    /// <summary>
    /// Repository người dùng
    /// </summary>
    public class UserRepo : BaseRepo, IUserRepo
    {
        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="configuration">Config của project</param>
        public UserRepo(IConfiguration configuration): base(configuration)
        {

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
    }
}
