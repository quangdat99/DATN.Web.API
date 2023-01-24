using DATN.Web.Service.Constants;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Service
{
    public class BaseService : IBaseService
    {
        #region DECLARE

        /// <summary>
        /// Base repo
        /// </summary>
        IBaseRepo _baseRepo;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="baseRepo"> Base repo</param>
        public BaseService(IBaseRepo baseRepo)
        {
            _baseRepo = baseRepo;
        }

        public BaseService()
        {
        }

        #endregion

        public async Task<object> InsertAsync<T>(object entity)
        {
            var res = await _baseRepo.InsertAsync<T>(entity);
            return res;
        }

        public async Task<object> UpdateAsync<T>(object entity)
        {
            var res = await _baseRepo.UpdateAsync<T>(entity);
            return res;
        }

        public async Task<IList> GetComboboxPaging<T>(string columns, string filter, string sort)
        {
            return await _baseRepo.GetComboboxPaging(typeof(T), columns, filter, sort);
        }

        public async Task<DAResult> GetDataTable<T>(FilterTable filterTable)
        {
            return await _baseRepo.GetDataTable<T>(filterTable);
        }

        public virtual async Task<T> SaveData<T>(T model, int mode)
        {
            if (mode == (int)ModelState.Add)
            {
                if (model.GetType().GetProperty("created_date") != null)
                {
                    model.GetType().GetProperty("created_date").SetValue(model, DateTime.Now);
                }
                return await _baseRepo.InsertAsync<T>(model);
            }
            else
            {
                return await _baseRepo.UpdateAsync<T>(model);
            }

        }

        public virtual async Task<bool> DeleteAsync(object entity)
        {
            var result = await _baseRepo.DeleteAsync(entity);
            return result;
        }
    }
}