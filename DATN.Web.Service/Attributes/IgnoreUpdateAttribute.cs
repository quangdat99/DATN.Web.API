using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Attributes
{
    /// <summary>
    /// Đánh dấu bỏ qua cập nhật
    /// Gắn với các trường tự sinh dữ liệu từ db
    /// </summary>
    public class IgnoreUpdateAttribute : Attribute
    {
    }
}
