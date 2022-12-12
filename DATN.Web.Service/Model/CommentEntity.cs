using DATN.Web.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng đánh giá, bình luận
    /// <summary>
    [Table("comment")]
    public class CommentEntity
    {
        /// <summary>
        /// Định danh của đánh giá, bình luận
        /// <summary>
        [Key]
        public Guid comment_id { get; set; }
        /// <summary>
        /// Định danh của người đánh giá
        /// <summary>
        public Guid user_id { get; set; }
        /// <summary>
        /// Định danh sản phẩm
        /// <summary>
        public Guid product_id { get; set; }
        /// <summary>
        /// Số sao đánh giá cho sản phẩm
        /// <summary>
        public int rate { get; set; }
        /// <summary>
        /// Nội dung bình luận
        /// <summary>
        public string content { get; set; }
        /// <summary>
        /// Ngày tạo đánh giá, bình luận
        /// <summary>
        public DateTime created_date { get; set; }
        /// <summary>
        /// Ảnh bình luận, đánh giá
        /// <summary>
        public string img_url { get; set; }
    }
}
