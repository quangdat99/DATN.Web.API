using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class ProductClient
    {
        public Guid product_id { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public DateTime? created_date { get; set; }
        public string img_url { get; set; }
        public decimal? sale_price_max { get; set; }
        public decimal? sale_price_min { get; set; }
        public decimal? sale_price_old { get; set; }
        /// <summary>
        /// Số sp trong 1 loại sản phẩm
        /// </summary>
        public int count_detail { get; set; }
        /// <summary>
        /// Mức độ nổi bật 1 --> 10
        /// </summary>
        public int outstanding { get; set; }
        /// <summary>
        /// Số sp đã bán
        /// </summary>
        public decimal? count_order { get; set; }
        public decimal? rate { get; set; }
        public decimal? product_discount { get; set; }
        public int totalRecord { get; set; }
        public string colors { get; set; }
        public string sizes { get; set; }
        public int count_comment { get; set; }
        public int total_quantity { get; set; }
        public string sale_price { get; set; }
        public string quantity { get; set; }
    }
}

