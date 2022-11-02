using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN.Web.Api.Controllers
{
    /// <summary>
    /// Controller Kích thước
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class SizeController : BaseController<SizeEntity>
    {
        /// <summary>
        /// Service Kích thước
        /// </summary>
        ISizeService _sizeService;

        /// <summary>
        /// Repo Kích thước
        /// </summary>
        ISizeRepo _sizeRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="SizeService"></param>
        /// <param name="SizeRepo"></param>
        public SizeController(ISizeService sizeService, ISizeRepo sizeRepo) : base(sizeService, sizeRepo)
        {
            _sizeService = sizeService;
            _sizeRepo = sizeRepo;
        }
    }
}
