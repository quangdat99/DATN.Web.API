using DATN.Web.Service.Constants;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Service
{
    public class AttributeService : BaseService, IAttributeService
    {
        IAttributeRepo _attributeRepo;
        public AttributeService(IAttributeRepo attributeRepo) : base(attributeRepo)
        {
            _attributeRepo = attributeRepo;
        }
        public async Task<AttributeEntity> SaveData(AttributeEntity model, int mode)
        {
            var size = await _attributeRepo.GetAsync<AttributeEntity>(nameof(AttributeEntity.attribute_name), model.attribute_name);
            if (size?.Count > 0 && size.Any(x => x.attribute_id != model.attribute_id))
            {
                throw new ValidateException($"Nhóm thuộc tính < {model.attribute_name} > đã tồn tại, vui lòng nhập nhóm thuộc tính khác.", model, int.Parse(ResultCode.DuplicateName));
            }
            if (mode == (int)ModelState.Add)
            {
                model.created_date = DateTime.Now;
                return await _attributeRepo.InsertAsync<AttributeEntity>(model);
            }
            else
            {
                return await _attributeRepo.UpdateAsync<AttributeEntity>(model);
            }

        }


        public override async Task<bool> DeleteAsync(object entity)
        {
            var attribute = JsonConvert.DeserializeObject<AttributeEntity>(JsonConvert.SerializeObject(entity));
            var existProductAttribute = await _attributeRepo.GetAsync<ProductAttributeEntity>(nameof(ProductAttributeEntity.attribute_id), attribute.attribute_id);
            if (existProductAttribute?.Count > 0)
            {
                throw new ValidateException($"Nhóm thuộc tính < {attribute.attribute_name} > đã có phát sinh, không thể xóa.", entity, int.Parse(ResultCode.Incurred));
            }
            var result = await _attributeRepo.DeleteAsync(attribute);
            return result;
        }
    }
}
