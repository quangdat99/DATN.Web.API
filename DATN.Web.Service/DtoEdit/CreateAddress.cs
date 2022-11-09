using System;

namespace DATN.Web.Service.DtoEdit;

public class CreateAddress
{
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
    public bool is_default { get; set; }
}