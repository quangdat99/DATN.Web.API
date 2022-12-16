using System;
using System.Collections.Generic;
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
            var newAddress = new AddressEntity();
            newAddress.address_id = Guid.NewGuid();
            newAddress.user_id = createAddress.user_id;
            newAddress.province = createAddress.province;
            newAddress.district = createAddress.district;
            newAddress.commune = createAddress.commune;
            newAddress.province_code = createAddress.province_code;
            newAddress.district_code = createAddress.district_code;
            newAddress.commune_code = createAddress.commune_code;
            newAddress.address_detail = createAddress.address_detail;
            newAddress.is_default = createAddress.is_default;
            newAddress.created_date = DateTime.Now;
            newAddress.name = createAddress.name;
            newAddress.phone = createAddress.phone;

            await _addressRepo.InsertAsync<AddressEntity>(newAddress);
            if (newAddress.is_default){
                var setDefaultRow = await _addressRepo.SetDefaultAddressForUser(createAddress.user_id, newAddress.address_id);
            }

            return newAddress;
        }

        /// <summary>
        /// Update Address
        /// </summary>
        public async Task<AddressEntity> UpdateAddress(UpdateAddress updateAddress)
        {
            var existedAddress = await _addressRepo.GetByIdAsync<AddressEntity>(updateAddress.address_id);

            if (existedAddress == null)
            {
                throw new ValidateException("Your address doesn't exist", "");
            }

            existedAddress.province = updateAddress.province;
            existedAddress.district = updateAddress.district;
            existedAddress.commune = updateAddress.commune;
            existedAddress.province_code = updateAddress.province_code;
            existedAddress.district_code = updateAddress.district_code;
            existedAddress.commune_code = updateAddress.commune_code;
            existedAddress.address_detail = updateAddress.address_detail;
            existedAddress.is_default = updateAddress.is_default;
            existedAddress.name = updateAddress.name;
            existedAddress.phone = updateAddress.phone;

            await _addressRepo.UpdateAsync<AddressEntity>(existedAddress);
            // Nếu đặt là địa chỉ mặc định thì set lại default đối với user_id
            if (existedAddress.is_default)
            {
                await _addressRepo.SetDefaultAddressForUser(existedAddress.user_id, existedAddress.address_id);
            }
            return existedAddress;
        }

        /// <summary>
        /// set default address
        /// </summary>
        /// <param name="address_id"></param>
        /// <returns></returns>
        public  async Task<int> SetDefaultAddressForUser( Guid user_id,Guid address_id)
        {
            var existedAddress = await _addressRepo.SetDefaultAddressForUser(user_id,address_id);

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

            if (existedAddress.is_default)
            {
                throw new ValidateException("You don't have permission to delete this address", "");
            }

            await _addressRepo.DeleteAsync(existedAddress);

            return existedAddress;
        }

        public async Task<List<AddressEntity>> GetAddresses(Guid user_id)
        {
            var addresses = await _addressRepo.GetAsync<AddressEntity>("user_id", user_id);
            addresses = addresses.OrderByDescending(x => x.is_default).ThenByDescending(x => x.created_date).ToList();
            return addresses;
        }
    }
}