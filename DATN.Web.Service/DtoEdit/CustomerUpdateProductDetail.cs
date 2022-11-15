using System;
using System.Numerics;

namespace DATN.Web.Service.DtoEdit
{
    public class CustomerUpdateProductDetail
    {
        /// <summary>
        /// Định danh kích cỡ của sản phẩm
        /// <summary>
        public Guid? size_id { get; set; }

        /// <summary>
        ///  Định danh màu sắc của sản phẩm
        /// <summary>
        public Guid? color_id { get; set; }

        /// <summary>
        /// Số lượng hiện tại của sản phẩm
        /// <summary>
        public int quantity { get; set; }
    }
}