using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Web.Service.Model;

namespace DATN.Web.Service.Service
{
    public class VoucherService : BaseService, IVoucherService
    {
        private IVoucherRepo _voucherRepo;
        public VoucherService(IVoucherRepo voucherRepo) : base(voucherRepo)
        {
            _voucherRepo = voucherRepo;
        }
        
        /// <summary>
        /// Get List Voucher
        /// </summary>
        public async Task<List<VoucherEntity>> GetListVoucher()
        {
            return await _voucherRepo.GetAsync<VoucherEntity>();
        }
    }
}
