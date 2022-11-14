using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;

namespace DATN.Web.Service.Service
{
    public class ProductService : BaseService, IProductService
    {
        private IProductRepo _productRepo;

        public ProductService(IProductRepo productRepo) : base(productRepo)
        {
            _productRepo = productRepo;
        }

        /// <summary>
        /// Get List Of Products
        /// </summary>
        /// <param name="cart_id">Cart Id</param>
        public async Task<List<ProductEntity>> GetListProductFromCart(Guid cart_id)
        {
            var res = await _productRepo.GetAsync<ProductCartEntity>("cart_id", cart_id);
            var list = new List<Guid>();
            foreach (var p in res)
            {
                list.Add(p.product_id);
            }

            var listProduct = await _productRepo.GetListProductInfo(list);
            return listProduct;
        }
    }
}