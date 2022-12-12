using System.Threading.Tasks;
using System;

namespace DATN.Web.Service.Interfaces.Repo
{
    /// <summary>
    /// Interface Repo Địa chỉ
    /// </summary>
    public interface IAddressRepo : IBaseRepo
    {
        /// <summary>
        /// set default address
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="address_id"></param>
        /// <returns></returns>
        Task<int> SetDefaultAddressForUser(Guid user_id, Guid address_id);
    }
    
}