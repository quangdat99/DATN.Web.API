using DATN.Web.Service.Interfaces.Repo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Repo.Repo
{
    /// <summary>
    /// Repository màu sắc
    /// </summary>
    public class ColorRepo : BaseRepo, IColorRepo
    {
        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="configuration">Config của project</param>
        public ColorRepo(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
