using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class CommentInfo
    {
        public Guid comment_id { get; set; }
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
        public string avatar { get; set; }
        public string color_name { get; set; }
        public string size_name { get; set; }
        public string email { get; set; }
        public int count_comment { get; set; }
    }
}
