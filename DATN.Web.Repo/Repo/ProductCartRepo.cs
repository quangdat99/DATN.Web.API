using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Repo.Repo
{
    /// <summary>
    /// Repository giỏ hàng
    /// </summary>
    public class ProductCartRepo : BaseRepo, IProductCartRepo
    {
        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="configuration">Config của project</param>
        public ProductCartRepo(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<List<ProductCart>> GetProductCart(Guid cartId)
        {
            var param = new Dictionary<string, object>()
            {
                { "$cartId", cartId },
            };
            var result = await this.Provider.QueryAsync<ProductCart>("Proc_ProductCart",
                param, CommandType.StoredProcedure);

            return result;
        }
    }
}
