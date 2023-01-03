using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
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
    }
}
