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
    /// Controller Màu sắc
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class ColorController : BaseController<ColorEntity>
    {
        /// <summary>
        /// Service Màu sắc
        /// </summary>
        IColorService _colorService;

        /// <summary>
        /// Repo Màu sắc
        /// </summary>
        IColorRepo _colorRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="ColorService"></param>
        /// <param name="ColorRepo"></param>
        public ColorController(IColorService colorService, IColorRepo colorRepo) : base(colorService, colorRepo)
        {
            _colorService = colorService;
            _colorRepo = colorRepo;
        }
    }
}
