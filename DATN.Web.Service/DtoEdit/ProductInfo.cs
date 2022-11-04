using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class ProductInfo : ProductEntity
    {
        public List<ProductDetailEntity> ProductDetails { get; set; }
        public List<AttributeEntity> Attributes { get; set; }
    }
}
