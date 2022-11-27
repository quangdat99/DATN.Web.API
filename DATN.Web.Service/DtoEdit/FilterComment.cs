using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class FilterComment
    {
        public Guid product_id { get; set; }
        public string filterCode { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
