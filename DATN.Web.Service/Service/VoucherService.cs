using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Service
{
    public class VoucherService : BaseService, IVoucherService
    {
        public VoucherService(IVoucherRepo voucherRepo) : base(voucherRepo)
        {
        }
    }
}
