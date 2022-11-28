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
    /// Controller giỏ hàng
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class ProductCartController : BaseController<ProductCartEntity>
    {
        /// <summary>
        /// Service giỏ hàng
        /// </summary>
        IProductCartService _ProductCartService;

        /// <summary>
        /// Repo giỏ hàng
        /// </summary>
        IProductCartRepo _ProductCartRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="ProductCartService"></param>
        /// <param name="ProductCartRepo"></param>
        public ProductCartController(IProductCartService ProductCartService, IProductCartRepo ProductCartRepo, IServiceProvider serviceProvider) : base(ProductCartService, ProductCartRepo, serviceProvider)
        {
            _ProductCartService = ProductCartService;
            _ProductCartRepo = ProductCartRepo;
        }
    }
}
