using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Interfaces.Repo;
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
    /// Repository Loại sản phẩm
    /// </summary>
    public class CategoryRepo : BaseRepo, ICategoryRepo
    {
        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="configuration">Config của project</param>
        public CategoryRepo(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<List<CategoryDto>> GetCategory()
        {
            var res = await this.Provider.QueryAsync<CategoryDto>("Proc_GetCategory",
                null, CommandType.StoredProcedure);
            return res;
        }
    }
}
