using DATN.Web.Service.Interfaces.Repo;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace DATN.Web.Repo.Repo
{
    /// <summary>
    /// Repository Đại chỉ
    /// </summary>
    public class AddressRepo : BaseRepo, IAddressRepo
    {
        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="configuration">Config của project</param>
        public AddressRepo(IConfiguration configuration) : base(configuration)
        {
        }

        public  async Task<int> SetDefaultAddressForUser(Guid userId,Guid addressId)
        {
            var param = new Dictionary<string, object>()
            {
                { "$user_id", userId },
                {"$address_id" , addressId}
            };
            var res = (await this.Provider.QueryAsync<int>("Proc_SetDefaultAddress",
                param, CommandType.StoredProcedure)).FirstOrDefault();
           
            return res;
        }
    }
}