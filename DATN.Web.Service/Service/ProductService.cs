using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
using Newtonsoft.Json;

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

        public async Task<object> SaveProduct(ProductEdit saveProduct, int mode)
        {
            var productExist = await _productRepo.GetAsync<ProductEntity>(nameof(ProductEntity.product_code), saveProduct.product_code);
            if (productExist?.Count > 0 && productExist.Any(x => x.product_id != saveProduct.product_id))
            {
                throw new ValidateException($"Mã sản phẩm < {saveProduct.product_code} > đã tồn tại, vui lòng nhập mã sản phẩm khác.", saveProduct, int.Parse(ResultCode.DuplicateName));
            }
            var product = new ProductEntity();
            product.product_id = saveProduct.product_id;
            product.product_code = saveProduct.product_code;
            product.product_name = saveProduct.product_name;
            product.summary = saveProduct.summary;
            product.description = saveProduct.description;
            product.outstanding = saveProduct.outstanding;
            product.status = saveProduct.status;
            product.created_date = saveProduct.created_date;
            if (mode == 1) // Thêm mới
            {
                product.created_date = DateTime.Now;
                await _productRepo.InsertAsync<ProductEntity>(product);
            }
            else if (mode == 2) // Sửa
            {
                await _productRepo.UpdateAsync<ProductEntity>(product);
            }


            var productDetails = new List<ProductDetailEntity>();

            foreach (var item in saveProduct.productDetails)
            {
                if (item.state == 3) // xóa
                {
                    await _productRepo.DeleteAsync(new ProductDetailEntity
                    {
                        product_detail_id = item.product_detail_id
                    });
                }
                else if (item.state == 1) // Thêm
                {
                    var insert = new ProductDetailEntity
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
                    };
                    await _productRepo.InsertAsync<ProductDetailEntity>(insert);

                }
                else if (item.state == 2) // cập nhật
                {
                    var update = new ProductDetailEntity
                    {
                        product_detail_id = item.product_detail_id,
                        product_id = product.product_id,
                        img_url = item.img_url,
                        sale_price = item.sale_price,
                        sale_price_old = item.sale_price_old,
                        purchase_price = item.purchase_price,
                        size_name = item.size_name,
                        color_name = item.color_name,
                        quantity = item.quantity,
                        created_date = item.created_date,
                        product_discount = item.product_discount
                    };
                    await _productRepo.UpdateAsync<ProductDetailEntity>(update);
                }

            }


            foreach (var item in saveProduct.attributes.OrderBy(x => x.state == 3))
            {
                if (item.state == 1) // Thêm mới
                {
                    var insert = new ProductAttributeEntity
                    {
                        product_attribute_id = Guid.NewGuid(),
                        product_id = product.product_id,
                        attribute_id = item.attribute_id,
                        value = item.value,
                        created_date = DateTime.Now
                    };
                    await _productRepo.InsertAsync<ProductAttributeEntity>(insert);
                }
                else if (item.state == 2) // Sửa
                {
                    var update = new ProductAttributeEntity
                    {
                        product_attribute_id = item.product_attribute_id,
                        product_id = product.product_id,
                        attribute_id = item.attribute_id,
                        value = item.value,
                        created_date = item.created_date
                    };
                    await _productRepo.UpdateAsync<ProductAttributeEntity>(update, nameof(ProductAttributeEntity.value));
                }
                else if (item.state == 3) // Xóa
                {
                    await _productRepo.DeleteAsync(new ProductAttributeEntity
                    {
                        product_attribute_id = item.product_attribute_id
                    });
                }
            }

            foreach (var item in saveProduct.categories.OrderBy(x => x.state == 3))
            {
                if (item.state == 1) // Thêm mới
                {
                    var insert = new ProductCategoryEntity
                    {
                        product_category_id = Guid.NewGuid(),
                        category_id = item.category_id,
                        product_id = product.product_id,
                    };
                    await _productRepo.InsertAsync<ProductCategoryEntity>(insert);
                }
                else if (item.state == 3) // Xóa
                {
                    await _productRepo.DeleteAsync(new ProductCategoryEntity
                    {
                        product_category_id = item.product_category_id
                    });
                }
            }


            var data = await this.GetProductEdit(product.product_id);
            return data;
        }

        public async Task<ProductEdit> GetProductEdit(Guid id)
        {
            var product = await _productRepo.GetByIdAsync<ProductEdit>(id);
            var productDetails = await _productRepo.GetAsync<ProductDetailDtoEdit>(nameof(ProductDetailDtoEdit.product_id), id);
            productDetails = productDetails.OrderBy(x => x.created_date).ToList();

            if (productDetails?.Count > 0)
            {
                product.color = string.Join(";", productDetails.Where(x => !string.IsNullOrEmpty(x.color_name)).Select(x => x.color_name).Distinct().ToArray());
                product.size = string.Join(";", productDetails.Where(x => !string.IsNullOrEmpty(x.size_name)).Select(x => x.size_name).Distinct().ToArray());
                product.productDetails = productDetails;
            }

            var attributeProducts = await _productRepo.GetProductAttribute(id);
            if (attributeProducts?.Count > 0)
            {
                product.attributes = attributeProducts;
            }

            var categoryProducts = await _productRepo.GetProductCategory(id);
            if (categoryProducts?.Count > 0)
            {
                product.categories = categoryProducts;
            }
            return product;
        }

        public async Task<string> NewCode()
        {
            var product = await _productRepo.GetProductLastest();
            if (product != null)
            {
                var code = product.product_code;
                int posittion = 0;
                string strValue = "";
                string strSuffix = "";
                string strPrefix = "";
                int iValue = 0;
                int iLengthOfValue = 0;

                bool isEndPrefix = false;
                bool isEndSuffix = false;
                bool isEndValue = false;
                int tempChar;

                while (posittion <= code.Length - 1)
                {
                    string strChar = code.Substring(posittion, 1);
                    if (isEndPrefix == false && !int.TryParse(strChar, out tempChar))
                    {
                        strPrefix += strChar;
                    } else
                    {
                        isEndPrefix = true;
                        if (isEndValue == false && int.TryParse(strChar, out tempChar))
                        {
                            strValue += strChar;
                        } else
                        {
                            isEndValue = true;
                            strSuffix += strChar;
                        }
                    }
                    posittion += 1;
                }
                int.TryParse(strValue, out iValue);
                iLengthOfValue = strValue.Length;

                var newCode = string.Empty;
                bool isExist = true;
                // Check trùng mã thì tăng giá trị đến khi không trùng nữa

                while (isExist == true)
                {
                    newCode = this.GenerateNewCode(strPrefix, iValue, strSuffix, iLengthOfValue);
                    var entityExist = await _productRepo.GetAsync<ProductEntity>(nameof(ProductEntity.product_code), newCode);
                    if (entityExist?.Count > 0)
                    {
                        isExist = true;
                        iValue += 1;
                    } else
                    {
                        isExist = false;
                    }
                }
                return newCode;

            } else
            {
                return "SP01";
            }
        }

        protected string GenerateNewCode(string prefix, int value, string suffix, int lengthOfValue)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(prefix))
            {
                sb.Append(prefix);
            }

            var val = (value + 1).ToString();
            if (lengthOfValue > 0)
            {
                var len = val.Length;
                if (len < lengthOfValue)
                {
                    sb.Append(new string('0', lengthOfValue - len));
                }
            }
            sb.Append(val);

            if (!string.IsNullOrEmpty(suffix))
            {
                sb.Append(suffix);
            }
            return sb.ToString();
        }
    }
}