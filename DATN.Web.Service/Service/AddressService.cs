using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Service
{
    public class AddressService : BaseService, IAddressService
    {
        public AddressService(IAddressRepo addressRepo) : base(addressRepo)
        {
        }
    }
}
