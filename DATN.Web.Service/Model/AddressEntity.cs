using System;
using DATN.Web.Service.Attributes;

namespace DATN.Web.Service.Model
{
    /// <summary>
    /// Bảng thông tin địa chỉ của người dùng
    /// <summary>
    [Table("address")]
    public class AddressEntity
    {
        /// <summary>
        /// Định danh địa chỉ
        /// <summary>
        [Key]
        public Guid address_id { get; set; }

        /// <summary>
        /// Định danh của người dùng
        /// <summary>
        public Guid user_id { get; set; }

        /// <summary>
        /// Tỉnh/ thành phố
        /// <summary>
        public string province { get; set; }

        /// <summary>
        /// quận/ huyện
        /// <summary>
        public string district { get; set; }

        /// <summary>
        /// xã/ phường
        /// <summary>
        public string commune { get; set; }

        /// <summary>
        /// Địa chỉ chi tiết
        /// <summary>
        public string address_detail { get; set; }

        /// <summary>
        /// Có là địa chỉ mặc định không
        /// <summary>
        public Boolean is_default { get; set; }
    }
}