using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Thông tin nhóm thuộc tính của sản phẩm
    /// <summary>
    [Table("attribute")]
    public class AttributeEntity
    {
        /// <summary>
        /// PK
        /// <summary>
        [Key]
        public Guid attribute_id { get; set; }
        /// <summary>
        /// 
        /// <summary>
        public string attribute_name { get; set; }
        /// <summary>
        /// 
        /// <summary>
        public bool status { get; set; }
        /// <summary>
        /// 
        /// <summary>
        public DateTime? created_date { get; set; }
    }
}
