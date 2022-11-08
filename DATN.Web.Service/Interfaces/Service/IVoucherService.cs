using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Interfaces.Service
{
    /// <summary>
    /// Interface service Mã giảm giá
    /// </summary>
    public interface IVoucherService : IBaseService
    {
        /// <summary>
        /// Get List Voucher
        /// </summary>
        Task<List<VoucherEntity>> GetListVoucher();
    }
}