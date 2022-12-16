using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Model;

namespace DATN.Web.Service.Interfaces.Service
{
    /// <summary>
    /// Interface service Địa chỉ
    /// </summary>
    public interface IAddressService : IBaseService
    {
        /// <summary>
        /// Create Address
        /// </summary>
        Task<AddressEntity> CreateAddress(CreateAddress createAddress);

        /// <summary>
        /// Update Address
        /// </summary>
        Task<AddressEntity> UpdateAddress(UpdateAddress updateAddress);

        /// <summary>
        /// Delete Address
        /// </summary>
        Task<AddressEntity> DeleteAddress(Guid address_id);
        /// <summary>
        /// set default address
        /// </summary>
        /// <param name="address_id"></param>
        /// <returns></returns>
        Task<int> SetDefaultAddressForUser(Guid user_id,Guid address_id);

        /// <summary>
        /// List Address
        /// </summary>
        Task<List<AddressEntity>> GetAddresses(Guid user_id);

    }
}

