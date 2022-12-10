using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Service
{
    public class ProductCartService : BaseService, IProductCartService
    {
        private IProductCartRepo _productCartRepo;
        public ProductCartService(IProductCartRepo ProductCartRepo) : base(ProductCartRepo)
        {
            _productCartRepo = ProductCartRepo;
        }

        public async Task<ProductCartEntity> AddToCart(AddToCart addToCart)
        {
            var productCart = new ProductCartEntity();
            productCart.cart_id = addToCart.cart_id;
            productCart.product_cart_id = Guid.NewGuid();
            productCart.quantity = addToCart.quantity;
            productCart.product_detail_id = addToCart.product_detail_id;
            productCart.created_date = DateTime.Now;

            var listPrductCart = await _productCartRepo.GetAsync<ProductCartEntity>("cart_id", addToCart.cart_id);

            if (listPrductCart?.Count > 0)
            {
                var productCartExist = listPrductCart.Where(x => x.product_detail_id == addToCart.product_detail_id).FirstOrDefault();
                // Nếu đã tồn tại sp trong giỏ thì cập nhật số lượng
                if (productCartExist != null)
                {
                    productCartExist.quantity += addToCart.quantity;
                    return await _productCartRepo.UpdateAsync<ProductCartEntity>(productCartExist);
                }
                else
                {
                    return await _productCartRepo.InsertAsync<ProductCartEntity>(productCart);
                }

            }
            else
            {
                return await _productCartRepo.InsertAsync<ProductCartEntity>(productCart);
            }
        }

        public async Task<List<object>> GetProductCart(Guid cartId)
        {
            var productCarts = await _productCartRepo.GetProductCart(cartId);
            return productCarts;
        }
    }
}
