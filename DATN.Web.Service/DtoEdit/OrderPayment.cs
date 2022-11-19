using System;

namespace DATN.Web.Service.DtoEdit;

public class OrderPayment
{
    /// <summary>
    /// Order Id
    /// <summary>
    public Guid order_id { get; set; }

    /// <summary>
    /// Voucher Id
    /// <summary>
    public Guid voucher_id { get; set; }
}