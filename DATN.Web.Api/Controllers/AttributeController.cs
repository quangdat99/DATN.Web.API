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
    /// Controller Thuộc tính sp
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class AttributeController : BaseController<AttributeEntity>
    {
        /// <summary>
        /// Service Thuộc tính sp
        /// </summary>
        IAttributeService _attributeService;

        /// <summary>
        /// Repo Thuộc tính sp
        /// </summary>
        IAttributeRepo _attributeRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="AttributeService"></param>
        /// <param name="AttributeRepo"></param>
        public AttributeController(IAttributeService attributeService, IAttributeRepo attributeRepo, IServiceProvider serviceProvider) : base(attributeService, attributeRepo, serviceProvider)
        {
            _attributeService = attributeService;
            _attributeRepo = attributeRepo;
        }
    }
}
