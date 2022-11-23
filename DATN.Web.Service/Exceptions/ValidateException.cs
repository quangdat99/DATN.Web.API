using DATN.Web.Service.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Exceptions
{
    public class ValidateException : Exception
    {
        public int resultCode { get; set; }
        public dynamic DataErr { get; set; }
        public ValidateException(string msg, dynamic data, int code = 400) : base(msg)
        {
            DataErr = data;
            resultCode = code;
        }
    }
}
