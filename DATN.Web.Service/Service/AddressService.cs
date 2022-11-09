using System;
using System.Linq;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;

namespace DATN.Web.Service.Service
{
    public class AddressService : BaseService, IAddressService
    {
        private IAddressRepo _addressRepo;

        public AddressService(IAddressRepo addressRepo) : base(addressRepo)
        {
            _addressRepo = addressRepo;
        }

        /// <summary>
        /// Create Address
        /// </summary>
        public async Task<AddressEntity> CreateAddress(CreateAddress createAddress)
        {
            var existedAddress =
                await _addressRepo.GetAsync<AddressEntity>("user_id", createAddress.user_id);

            if (existedAddress.Count != 0)
            {
                var existedDefaultAddress =
                    existedAddress.FirstOrDefault(a => a.is_default);

                if (existedDefaultAddress != null && createAddress.is_default)
                {
                    throw new ValidateException("Your default address has existed", "");
                }
            }
            
            var newAddress = new AddressEntity();
            newAddress.address_id = Guid.NewGuid();
            newAddress.user_id = createAddress.user_id;
            newAddress.province = createAddress.province;
            newAddress.district = createAddress.district;
            newAddress.commune = createAddress.commune;
            newAddress.address_detail = createAddress.address_detail;
            newAddress.is_default = createAddress.is_default;

            await _addressRepo.InsertAsync<AddressEntity>(newAddress);

            return newAddress;
        }

        /// <summary>
        /// Update Address
        /// </summary>
        public async Task<AddressEntity> UpdateAddress(UpdateAddress updateAddress, Guid address_id)
        {
            var existedAddress = await _addressRepo.GetByIdAsync<AddressEntity>(address_id);

            if (existedAddress == null)
            {
                throw new ValidateException("Your address doesn't exist", "");
            }

            existedAddress.user_id = (updateAddress.user_id != null) ? updateAddress.user_id : existedAddress.user_id;
            existedAddress.province =
                (updateAddress.province != null) ? updateAddress.province : existedAddress.province;
            existedAddress.district =
                (updateAddress.district != null) ? updateAddress.district : existedAddress.district;
            existedAddress.commune = (updateAddress.commune != null) ? updateAddress.commune : existedAddress.commune;
            existedAddress.address_detail = (updateAddress.address_detail != null)
                ? updateAddress.address_detail
                : existedAddress.address_detail;

            existedAddress.is_default = updateAddress.is_default;

            await _addressRepo.UpdateAsync<AddressEntity>(existedAddress);
            return existedAddress;
        }

        /// <summary>
        /// Delete Address
        /// </summary>
        public async Task<AddressEntity> DeleteAddress(Guid address_id)
        {
            var existedAddress = await _addressRepo.GetByIdAsync<AddressEntity>(address_id);

            if (existedAddress == null)
            {
                throw new ValidateException("Your address doesn't exist", "");
            }

            await _addressRepo.DeleteAsync(existedAddress);

            return existedAddress;
        }
    }
}