using DATN.Web.Service.Constants;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Service
{
    public class CategoryService : BaseService, ICategoryService
    {
        private ICategoryRepo _categoryRepo;
        public CategoryService(ICategoryRepo categoryRepo) : base(categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<List<CategoryDto>> GetCategory()
        {
            var data = await _categoryRepo.GetCategory();
            return data;
        }
        public async Task<CategoryEntity> SaveData(CategoryEntity model, int mode)
        {
            var size = await _categoryRepo.GetAsync<CategoryEntity>(nameof(CategoryEntity.category_name), model.category_name);
            if (size?.Count > 0 && size.Any(x => x.category_id != model.category_id))
            {
                throw new ValidateException($"Loại sản phẩm < {model.category_name} > đã tồn tại, vui lòng nhập loại sản phẩm khác.", model, int.Parse(ResultCode.DuplicateName));
            }
            if (mode == (int)ModelState.Add)
            {
                model.created_date = DateTime.Now;
                return await _categoryRepo.InsertAsync<CategoryEntity>(model);
            }
            else
            {
                return await _categoryRepo.UpdateAsync<CategoryEntity>(model);
            }

        }

        public override async Task<bool> DeleteAsync(object entity)
        {
            var category = JsonConvert.DeserializeObject<CategoryEntity>(JsonConvert.SerializeObject(entity));
            var existProductCategory = await _categoryRepo.GetAsync<ProductCategoryEntity>(nameof(ProductCategoryEntity.category_id), category.category_id);
            if (existProductCategory?.Count > 0)
            {
                throw new ValidateException($"Loại sản phẩm < {category.category_name} > đã có phát sinh, không thể xóa.", entity, int.Parse(ResultCode.Incurred));
            }
            var result = await _categoryRepo.DeleteAsync(category);
            return result;
        }
    }
}
