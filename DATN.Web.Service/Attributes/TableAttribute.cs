using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Attributes
{
    /// <summary>
    /// Attribute đánh dấu tên bảng
    /// </summary>
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// Tên bảng chi tiết
        /// </summary>
        public string Table { get; set; }
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="table">Tên bảng</param>
        public TableAttribute(string table)
        {
            Table = table;
        }
    }
}
