using DATN.Web.Service.Model;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DATN.Web.Service.Properties;
using System.Linq;
using System.Threading.Tasks;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Contexts;
using Microsoft.Extensions.DependencyInjection;
using DATN.Web.Service.DtoEdit;

namespace DATN.Web.Api.Controllers
{
    /// <summary>
    /// Base Controller
    /// </summary>
    /// <typeparam name="T">Một thực thể</typeparam>
    [ApiController]
    public class BaseController<T> : ControllerBase where T : class
    {
        #region DECLARE
        /// <summary>
        /// Base service
        /// </summary>
        IBaseService _baseService;

        /// <summary>
        /// Base Repo
        /// </summary>
        IBaseRepo _baseRepo;
        protected readonly IContextService _contextService;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="baseService"></param>
        public BaseController(IBaseService baseService, IBaseRepo baseRepo, IServiceProvider serviceProvider)
        {
            _baseService = baseService;
            _baseRepo = baseRepo;
            _contextService = serviceProvider.GetRequiredService<IContextService>();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Lấy tất cả thực thể T
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var abc = _contextService.Get();
            try
            {
                var res = await _baseRepo.GetAsync<T>();
                if (res?.Count > 0)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", new List<T>());
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, new List<T>());
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Lấy thông tin thực thể t
        /// </summary>
        /// <param name="id">id thực thể</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {

            try
            {
                var res = await _baseRepo.GetByIdAsync<T>(id);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", null);
                    return Ok(actionResult);
                }
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Insert một thực thể t
        /// </summary>
        /// <param name="t">Thông tin thực thể t</param>
        [HttpPost]
        public async Task<IActionResult> Insert(T t)
        {
            try
            {
                var row = await _baseService.InsertAsync<T>(t);
                if (row != null)
                {
                    var actionResult = new DAResult(200, Resources.addDataSuccess, "", row);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.addDataFail, "", null);
                    return Ok(actionResult);
                }
            }
            catch (ValidateException exception)
            {
                var actionResult = new DAResult(400, exception.Message, "", exception.DataErr);
                return Ok(actionResult);

            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Update một thực thể t
        /// </summary>
        /// <param name="t">Thông tin thực thể t</param>
        [HttpPut]
        public async Task<IActionResult> Update(T t)
        {
            try
            {
                var row = await _baseService.UpdateAsync<T>(t);
                if (row != null)
                {
                    var actionResult = new DAResult(200, Resources.editDataSuccess, "", row);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.editDataFail, "", null);
                    return Ok(actionResult);
                }
            }
            catch (ValidateException exception)
            {
                var actionResult = new DAResult(400, exception.Message, "", 0);
                return Ok(actionResult);
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, 0);
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Delete một thực thể t
        /// </summary>
        /// <param name="id">id thực thể</param>
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(T t)
        {
            try
            {
                var result = await _baseService.DeleteAsync(t);
                if (result)
                {
                    var actionResult = new DAResult(200, Resources.deleteDataSuccess, "", result);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.deleteDataFail, "", false);
                    return Ok(actionResult);
                }
            }
            catch (ValidateException exception)
            {
                var actionResult = new DAResult(exception.resultCode, exception.Message, "", exception.DataErr);
                return Ok(actionResult);
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Lấy dữ liệu combobox
        /// </summary>
        [HttpPost("combobox")]
        public async Task<IActionResult> GetComboboxPaging([FromBody] PagingParameter param)
        {
            try
            {
                var result = await _baseService.GetComboboxPaging<T>(param.Columns, param.Filter, param.Sort);
                return Ok(new DAResult(200, Resources.getDataSuccess, "", result));
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Lấy dữ liệu bảng
        /// </summary>
        [HttpPost("dataTable")]
        public async Task<IActionResult> GetDataTable([FromBody] FilterTable param)
        {
            try
            {
                var result = await _baseService.GetDataTable<T>(param);
                return Ok(result);
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }

        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        [HttpPost("saveData/{mode}")]
        public virtual async Task<IActionResult> SaveData([FromBody] T model, int mode)
        {
            try
            {
                var result = await _baseService.SaveData<T>(model, mode);
                return Ok(new DAResult(200, Resources.addDataSuccess, "", result));
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                return Ok(actionResult);
            }
        }

        #endregion
    }
}
