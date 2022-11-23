using System;
using System.Numerics;

namespace DATN.Web.Service.DtoEdit
{
    public class CustomerUpdateProductDetail
    {
        /// <summary>
        /// kích cỡ của sản phẩm
        /// <summary>
        public string size_name { get; set; }

        /// <summary>
        ///  màu sắc của sản phẩm
        /// <summary>
        public string color_name { get; set; }

        /// <summary>
        /// Số lượng hiện tại của sản phẩm
        /// <summary>
        public int quantity { get; set; }
    }
}