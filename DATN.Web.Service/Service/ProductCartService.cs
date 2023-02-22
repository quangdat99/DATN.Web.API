using DATN.Web.Service.Constants;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using Newtonsoft.Json;
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
            productCart.product_id = addToCart.product_id;

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

        public async Task<int> Checkout(Checkout checkout)
        {
            var address = await _productCartRepo.GetByIdAsync<AddressEntity>(checkout.address_id);
            var order = new OrderEntity();
            order.user_id = checkout.user_id;
            order.order_id = Guid.NewGuid();
            order.status = OrderStatus.Acceipt;
            order.user_name = address.name;
            order.created_date = DateTime.Now;
            order.phone = address.phone;
            order.address = $"{address.address_detail}, {address.commune}, {address.district}, {address.province}";
            order.product_amount = 0;
            order.total_amount = 0;
            order.voucher_amount = 0;
            order.order_code = DateTime.Now.ToString("yyyyMMddHHmmss");

            var productOrder = new List<ProductOrderEntity>();
            if (checkout.mode == 1)
            {

                var productCarts = await _productCartRepo.GetProductCart(checkout.cart_id);

                foreach (var p in checkout.listProduct)
                {
                    var productDetail = await _productCartRepo.GetByIdAsync<ProductDetailEntity>(p.product_detail_id);
                    if (productDetail != null && p.quantity <= productDetail.quantity)
                    {
                        var productCart = productCarts.Find(x => x.product_cart_id == p.product_cart_id);
                        if (productCart != null)
                        {
                            var item = new ProductOrderEntity()
                            {
                                product_order_id = Guid.NewGuid(),
                                order_id = order.order_id,
                                product_id = p.product_id,
                                product_detail_id = p.product_detail_id,
                                product_amount = productCart.sale_price,
                                product_name = productCart.product_name,
                                color_name = productCart.color_name,
                                size_name = productCart.size_name,
                                url_img = productCart.img_url,
                                quantity = productCart.quantity,
                                product_amount_old = productCart.sale_price_old ?? 0,
                                purchase_amount = productDetail.purchase_price
                            };
                            productOrder.Add(item);
                            productDetail.quantity = productDetail.quantity - productCart.quantity;
                            order.product_amount += item.product_amount * item.quantity;
                            order.total_amount += item.product_amount * item.quantity;
                            var param = new ProductCartEntity()
                            {
                                product_cart_id = productCart.product_cart_id ?? Guid.NewGuid(),
                            };
                            await _productCartRepo.DeleteAsync(param);
                        }


                        await _productCartRepo.UpdateAsync<ProductDetailEntity>(productDetail, "quantity");
                    }
                    else
                    {
                        var delete = JsonConvert.DeserializeObject<ProductCartEntity>(JsonConvert.SerializeObject(p));
                        await _productCartRepo.DeleteAsync(delete);
                        if (productDetail == null)
                        {

                            throw new ValidateException($"Sản phẩm {p.product_name} không tồn tại", p, 400);
                        }
                        if (p.quantity > productDetail.quantity)
                        {
                            throw new ValidateException($"Sản phẩm {p.product_name} vượt quá số lượng", p, 400);

                        }
                    }
                }

            }
            else
            {
                foreach (var p in checkout.listProduct)
                {
                    var productDetail = await _productCartRepo.GetByIdAsync<ProductDetailEntity>(p.product_detail_id);
                    if (productDetail != null && p.quantity <= productDetail.quantity)
                    {
                        var item = new ProductOrderEntity()
                        {
                            product_order_id = Guid.NewGuid(),
                            order_id = order.order_id,
                            product_id = p.product_id,
                            product_detail_id = p.product_detail_id,
                            product_amount = p.sale_price,
                            product_name = p.product_name,
                            color_name = p.color_name,
                            size_name = p.size_name,
                            url_img = p.img_url,
                            quantity = p.quantity,
                            product_amount_old = p.sale_price_old ?? 0,
                            purchase_amount = productDetail.purchase_price
                        };
                        productOrder.Add(item);
                        productDetail.quantity = productDetail.quantity - p.quantity;
                        order.product_amount += item.product_amount * item.quantity;
                        order.total_amount += item.product_amount * item.quantity;

                        await _productCartRepo.UpdateAsync<ProductDetailEntity>(productDetail, "quantity");
                    }
                    else
                    {

                        if (productDetail == null)
                        {

                            throw new ValidateException($"Sản phẩm <{p.product_name}> không tồn tại", p, 400);
                        }
                        if (p.quantity > productDetail.quantity)
                        {
                            throw new ValidateException($"Sản phẩm <{p.product_name}> vượt quá số lượng", p, 400);

                        }
                    }
                }

            }

            await _productCartRepo.InsertAsync<OrderEntity>(order);
            foreach (var item in productOrder)
            {
                await _productCartRepo.InsertAsync<ProductOrderEntity>(item);
            }

            return 1;
        }

        public async Task<bool> DeleteProductCart(Guid id)
        {
            var param = new ProductCartEntity()
            {
                product_cart_id = id
            };
            var data = await _productCartRepo.DeleteAsync(param);
            return data;
        }

        public async Task<List<ProductCart>> GetProductCart(Guid cartId)
        {
            var productCarts = await _productCartRepo.GetProductCart(cartId);
            foreach (var productCart in productCarts)
            {
                var productDetails = _productCartRepo.GetByIdAsync<ProductDetailEntity>(productCart.product_detail_id);
                if (productDetails == null || productCart.quantity > productCart.quantity_max)
                {
                    var item = JsonConvert.DeserializeObject<ProductCartEntity>(JsonConvert.SerializeObject(productCart));
                    await _productCartRepo.DeleteAsync(item);
                }
            }
            productCarts = await _productCartRepo.GetProductCart(cartId);
            return productCarts;
        }

        public async Task<ProductCartEntity> UpdateQuantity(ProductCartDto mdoel)
        {
            var param = new ProductCartEntity()
            {
                product_cart_id = mdoel.product_cart_id,
                quantity = mdoel.quantity
            };
            var data = await _productCartRepo.UpdateAsync<ProductCartEntity>(param, "quantity");
            return data;
        }
    }
}
