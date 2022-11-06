using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN.Web.Api.Controllers
{
    /// <summary>
    /// Controller Đại chỉ
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class AddressController : BaseController<AddressEntity>
    {
        /// <summary>
        /// Service Đại chỉ
        /// </summary>
        IAddressService _addressService;

        /// <summary>
        /// Repo Đại chỉ
        /// </summary>
        IAddressRepo _addressRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="AddressService"></param>
        /// <param name="AddressRepo"></param>
        public AddressController(IAddressService addressService, IAddressRepo addressRepo, IServiceProvider serviceProvider) : base(addressService, addressRepo, serviceProvider)
        {
            _addressService = addressService;
            _addressRepo = addressRepo;
        }
    }
}
