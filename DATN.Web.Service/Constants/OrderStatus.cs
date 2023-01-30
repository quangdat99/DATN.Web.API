namespace DATN.Web.Service.Constants
{
    /// <summary>
    /// Trạng thái đơn hàng
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Tất cả
        /// </summary>
        All = 0,

        /// <summary>
        /// chờ xác nhận
        /// </summary>
        Acceipt = 5,

        /// <summary>
        /// chờ lấy hàng
        /// </summary>
        Pending = 2,

        /// <summary>
        /// Đang giao
        /// </summary>
        Delivering = 3,

        /// <summary>
        /// Giao thành công
        /// </summary>
        Delivered = 1,

        /// <summary>
        /// Đã hủy đơn
        /// </summary>
        Cancelled = 4,

        /// <summary>
        /// Giao hàng thất bại/Trả hàng
        /// </summary>
        Undelivered = 6,

        /// <summary>
        /// Hoàn hàng trở lại cửa hàng
        /// </summary>
        Refund = 7
    }
}