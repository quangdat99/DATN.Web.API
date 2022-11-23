using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class SearchModel
    {
        public string keyword { get; set; }
        public int? rating { get; set; }
        public decimal? fromAmount { get; set; }
        public decimal? toAmount { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        /// <summary>
        /// sắp xếp:  1: Mới nhất, 2: Báy chạy, 3: Giá Thấp đến cao, 4: Giá Cao đến thấp
        /// </summary>
        public int? sort { get; set; } 
        public string category { get; set; }
    }
}