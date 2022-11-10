﻿using System;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Web.Api.Controllers
{
    /// <summary>
    /// Controller Đại chỉ
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]es")]
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
        public AddressController(IAddressService addressService, IAddressRepo addressRepo,
            IServiceProvider serviceProvider) : base(addressService, addressRepo, serviceProvider)
        {
            _addressService = addressService;
            _addressRepo = addressRepo;
        }

        /// <summary>
        /// Create An Address Of User
        /// </summary>
        /// <param name="createAddress">CreareAddress</param>
        [HttpPost("create")]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddress createAddress)
        {
            try
            {
                var res = await _addressService.CreateAddress(createAddress);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", null);
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }
        
        /// <summary>
        /// Update An Address Of User
        /// </summary>
        /// <param name="updateAddress">UpdateAddress</param>
        /// <param name="address_id">address_id</param>
        [HttpPut("update/{address_id}")]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddress updateAddress, Guid address_id)
        {
            try
            {
                var res = await _addressService.UpdateAddress(updateAddress, address_id);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", null);
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }
        
        /// <summary>
        /// Update An Address Of User
        /// </summary>
        /// <param name="address_id">address_id</param>
        [HttpDelete("delete/{address_id}")]
        public async Task<IActionResult> DeleteAddress(Guid address_id)
        {
            try
            {
                var res = await _addressService.DeleteAddress(address_id);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", null);
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }
    }
}