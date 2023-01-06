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

        public async Task<object> SaveProduct(SaveProduct saveProduct)
        {
            var product = new ProductEntity();
            product.product_id = Guid.NewGuid();
            product.product_code = saveProduct.product_code;
            product.product_name = saveProduct.product_name;
            product.summary = saveProduct.summary;
            product.description = saveProduct.description;
            product.created_date = DateTime.Now;
            product.status = 1; // Đang bán

            await _productRepo.InsertAsync<ProductEntity>(product);

            var productDetails = new List<ProductDetailEntity>();

            foreach (var item in saveProduct.ProductDetails)
            {
                productDetails.Add(new ProductDetailEntity
                {
                    product_detail_id = Guid.NewGuid(),
                    product_id = product.product_id,
                    img_url = item.img_url,
                    sale_price = item.sale_price,
                    sale_price_old = item.sale_price_old,
                    purchase_price = item.purchase_price,
                    size_name = item.size_name,
                    color_name = item.color_name,
                    quantity = item.quantity,
                    created_date = DateTime.Now,
                    product_discount = item.product_discount
                });
            }

            foreach (var item in productDetails)
            {
                await _productRepo.InsertAsync<ProductDetailEntity>(item);
            }
            var data = await _productRepo.GetProductInfo(product.product_id);
            return data;
        }
    }
}