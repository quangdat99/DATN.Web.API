using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using System;
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
    }
}