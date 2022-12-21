using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class UserInfo: UserEntity
    {
        public string day { get; set; }
        public string month { get; set; }
        public string year { get; set; }
    }
}
