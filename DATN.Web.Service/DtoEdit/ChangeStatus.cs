using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class ChangeStatus
    {
        public Guid order_id { get; set; }
        public int status { get; set; }
    }
}
