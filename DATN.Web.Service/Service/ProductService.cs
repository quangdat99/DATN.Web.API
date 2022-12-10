using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
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
                list.Add(p.product_detail_id);
            }

            var listProduct = await _productRepo.GetListProductInfo(list);
            return listProduct;
        }

        /// <summary>
        /// Delete Product Detail
        /// </summary>
        /// <param name="productDetailId">Product Detail Id</param>
        public async Task<ProductDetailEntity> DeleteProductDetail(Guid productDetailId)
        {
            var existedProductDetail = await _productRepo.GetByIdAsync<ProductDetailEntity>(productDetailId);

            if (existedProductDetail == null)
            {
                throw new ValidateException("Your product detail doesn't exist", "");
            }

            await _productRepo.DeleteAsync(existedProductDetail);

            return existedProductDetail;
        }

        /// <summary>
        /// Customer Update Product Detail In Cart
        /// </summary>
        /// <param name="productDetailId">Product Detail Id</param>
        /// <param name="customerUpdateProductDetail">Update Info</param>
        public async Task<ProductDetailEntity> CustomerUpdateProductDetail(Guid productDetailId,
            CustomerUpdateProductDetail customerUpdateProductDetail)
        {
            var existedProductDetail = await _productRepo.GetByIdAsync<ProductDetailEntity>(productDetailId);

            if (existedProductDetail == null)
            {
                throw new ValidateException("Your product detail doesn't exist", "");
            }

            existedProductDetail.size_name = (customerUpdateProductDetail.size_name != null)
                ? customerUpdateProductDetail.size_name
                : existedProductDetail.size_name;
            existedProductDetail.color_name = (customerUpdateProductDetail.color_name != null)
                ? customerUpdateProductDetail.color_name
                : existedProductDetail.color_name;

            if (customerUpdateProductDetail.quantity > 0)
            {
                existedProductDetail.quantity = customerUpdateProductDetail.quantity;
            }
            else
            {
                throw new ValidateException("Quantity cannot be lower than 1", "");
            }

            await _productRepo.UpdateAsync<ProductDetailEntity>(existedProductDetail);

            return existedProductDetail;
        }

        /// <summary>
        /// Add Single Product ToCart
        /// </summary>
        /// <param name="cartId">Cart ID</param>
        /// <param name="newProductId">New Product Id</param>
        public async Task<List<ProductEntity>> AddSingleProductToCart(Guid cartId,
            Guid newProductId)
        {
            var existedProductList = await GetListProductFromCart(cartId);

            var newProduct = await _productRepo.GetByIdAsync<ProductEntity>(newProductId);

            if (existedProductList.Contains(newProduct))
            {
                var existedProductDetail =
                    await _productRepo.GetAsync<ProductDetailEntity>("product_id", newProduct.product_id);
                var res = existedProductDetail.FirstOrDefault();

                if (res != null)
                {
                    res.quantity++;
                    await _productRepo.UpdateAsync<ProductDetailEntity>(res);
                }
                else
                {
                    throw new ValidateException("Product hasn't had detail yet", "");
                }
            }
            else
            {
                existedProductList.Add(newProduct);
                var newProductCart = new ProductCartEntity();
                newProductCart.product_cart_id = Guid.NewGuid();
                newProductCart.cart_id = cartId;
                newProductCart.product_detail_id = newProductId;

                await _productRepo.InsertAsync<ProductCartEntity>(existedProductList);
            }

            return existedProductList;
        }

        public async Task<List<ProductClient>> GetProductHome(SearchModel model)
        {
            var products = await _productRepo.GetProductHome(model);
            return products;
        }
    }
}