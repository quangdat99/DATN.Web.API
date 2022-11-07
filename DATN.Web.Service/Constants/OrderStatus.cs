namespace DATN.Web.Service.Constants
{
    /// <summary>
    /// Trạng thái đơn hàng
    /// </summary>
    public enum OrderStatus
    {
        /// Tất cả
        All = 0,
        /// chờ lấy hàng
        Pending = 1, 
        /// Đang giao
        Delivering = 2,
        /// Giao thành công
        Delivered = 3,
        /// Đã hủy đơn
        Cancelled = 4,
        // Giao hàng thất bại
        Undelivered = 5
    } 
}