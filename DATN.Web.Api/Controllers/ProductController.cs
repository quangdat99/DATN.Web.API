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
    /// Controller Sản phẩm
    /// </summary>
    /// CreatedBy: dqdat (20/07/2021)
    [Route("api/[controller]s")]
    public class ProductController : BaseController<ProductEntity>
    {
        /// <summary>
        /// Service  Sản phẩm
        /// </summary>
        IProductService _productService;

        /// <summary>
        /// Repo  Sản phẩm
        /// </summary>
        IProductRepo _productRepo;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="ProductService"></param>
        /// <param name="ProductRepo"></param>
        public ProductController(IProductService productService, IProductRepo productRepo) : base(productService, productRepo)
        {
            _productService = productService;
            _productRepo = productRepo;
        }
    }
}
