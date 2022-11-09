using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATN.Web.Service.Properties;

namespace DATN.Web.Api.Controllers
{
    /// <summary>
    /// Controller Mã giảm giá
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class VoucherController : BaseController<VoucherEntity>
    {
        /// <summary>
        /// Service  Mã giảm giá
        /// </summary>
        IVoucherService _voucherService;

        /// <summary>
        /// Repo  Mã giảm giá
        /// </summary>
        IVoucherRepo _voucherRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="VoucherService"></param>
        /// <param name="VoucherRepo"></param>
        public VoucherController(IVoucherService voucherService, IVoucherRepo voucherRepo, IServiceProvider serviceProvider) : base(voucherService, voucherRepo, serviceProvider)
        {
            _voucherService = voucherService;
            _voucherRepo = voucherRepo;
        }
        
        /// <summary>
        /// Get List Voucher
        /// </summary>
        [HttpGet("listVoucher")]
        public async Task<IActionResult> ListVoucher()
        {
            try
            {
                var res = await _voucherService.GetListVoucher();
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
