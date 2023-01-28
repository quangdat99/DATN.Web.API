using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class ProductEdit : ProductEntity
    {
        public string color { get; set; } = string.Empty;
        public string size { get; set; } = string.Empty;
        public List<ProductDetailDtoEdit> productDetails { get; set; } = new List<ProductDetailDtoEdit>();
        public List<AttributeClient> attributes { get; set; } = new List<AttributeClient>();
        public List<CategoryDtoEdit> categories { get; set; } = new List<CategoryDtoEdit>();
    }

    public class CategoryDtoEdit : ProductCategoryEntity
    {
        public string category_name { get; set; }
        public int state { get; set; } = 2;
    }

    public class ProductDetailDtoEdit : ProductDetailEntity
    {
        public int state { get; set; } = 2;
    }
}
