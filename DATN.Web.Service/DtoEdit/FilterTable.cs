using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class FilterTable
    {
        public List<string> fields { get; set; }

        public List<string> sortBy { get; set; }
        public List<string> sortType { get; set; }
        public string filter { get; set; }

        public int page { get; set; }
        public int size { get; set; }
    }
}
