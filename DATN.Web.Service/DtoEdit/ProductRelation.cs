using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class ProductRelation
    {
        public Guid product_id { get; set; }

        /// <summary>
        /// Số lần xuất hiện
        /// </summary>
        public int count_relation { get; set; }
    }
}
