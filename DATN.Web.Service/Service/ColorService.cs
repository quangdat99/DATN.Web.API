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
    public class ColorService : BaseService, IColorService
    {
        IColorRepo _colorRepo;
        public ColorService(IColorRepo colorRepo) : base(colorRepo)
        {
            _colorRepo = colorRepo;
        }
        public async Task<ColorEntity> SaveData(ColorEntity model, int mode)
        {
            var color = await _colorRepo.GetAsync<ColorEntity>(nameof(ColorEntity.color_name), model.color_name);
            if (color?.Count > 0 && color.Any(x => x.color_id != model.color_id))
            {
                throw new ValidateException($"Màu sắc < {model.color_name} > đã tồn tại, vui lòng nhập màu sắc khác.", model, int.Parse(ResultCode.DuplicateName));
            }
            if (mode == (int)ModelState.Add)
            {
                model.created_date = DateTime.Now;
                return await _colorRepo.InsertAsync<ColorEntity>(model);
            }
            else
            {
                return await _colorRepo.UpdateAsync<ColorEntity>(model);
            }

        }
    }
}
