using System;
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
        Task<AddressEntity> UpdateAddress(UpdateAddress updateAddress, Guid address_id);

        /// <summary>
        /// Delete Address
        /// </summary>
        Task<AddressEntity> DeleteAddress(Guid address_id);


    }
}
