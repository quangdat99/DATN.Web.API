using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class PagingParameter
    {
        /// <summary>
        /// Điều kiện lọc
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// Danh sách các cột trả về
        /// </summary>
        public string Columns { get; set; }
        /// <summary>
        /// Điều kiện sắp xếp
        /// </summary>
        public string Sort { get; set; }
    }
}
