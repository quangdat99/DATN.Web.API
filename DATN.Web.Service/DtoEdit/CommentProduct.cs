using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class CommentProduct
    {
        public Guid order_id { get; set; }
        public List<CommentEntity> commentProducts { get; set; } = new List<CommentEntity>();
    }
}
