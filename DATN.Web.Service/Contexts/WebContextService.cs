using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Contexts
{
    public class WebContextService : IContextService
    {
        private ContextData _contextData = null;
        public WebContextService()
        {

        }
        public void Set(ContextData contextData)
        {
            _contextData = contextData;
        }

        public ContextData Get()
        {
            return _contextData;
        }
    }
}
