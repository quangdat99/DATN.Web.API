namespace DATN.Web.Service.Constants
{
    /// <summary>
    /// Trạng thái đơn hàng
    /// </summary>
    public enum ProductStatus
    {
        /// <summary>
        /// Tất cả
        /// </summary>
        All = 0,

        /// <summary>
        /// chờ lấy hàng
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Đang giao
        /// </summary>
        Delivering = 2,

        /// <summary>
        /// Giao thành công
        /// </summary>
        Delivered = 3,

        /// <summary>
        /// Đã hủy đơn
        /// </summary>
        Cancelled = 4,

        /// <summary>
        /// Giao hàng thất bại
        /// </summary>
        Undelivered = 5
    }
}