using DATN.Web.Service.Constants;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Service
{
    public class SizeService : BaseService, ISizeService
    {
        ISizeRepo _sizeRepo;
        public SizeService(ISizeRepo sizeRepo) : base(sizeRepo)
        {
            _sizeRepo = sizeRepo;
        }
        public async Task<SizeEntity> SaveData(SizeEntity model, int mode)
        {
            var size = await _sizeRepo.GetAsync<SizeEntity>(nameof(SizeEntity.size_name), model.size_name);
            if (size?.Count > 0 && size.Any(x => x.size_id != model.size_id))
            {
                throw new ValidateException($"Kích cỡ < {model.size_name} > đã tồn tại, vui lòng nhập kích cỡ khác.", model, int.Parse(ResultCode.DuplicateName));
            }
            if (mode == (int)ModelState.Add)
            {
                model.created_date = DateTime.Now;
                return await _sizeRepo.InsertAsync<SizeEntity>(model);
            }
            else
            {
                return await _sizeRepo.UpdateAsync<SizeEntity>(model);
            }

        }
    }
}
