using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Exceptions
{
    public class ValidateException : Exception
    {

        public dynamic DataErr { get; set; }
        public ValidateException(string msg, dynamic data) : base(msg)
        {
            DataErr = data;
        }
    }
}
