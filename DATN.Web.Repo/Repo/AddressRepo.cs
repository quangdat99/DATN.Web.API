using DATN.Web.Service.Interfaces.Repo;
using Microsoft.Extensions.Configuration;

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
    }
}