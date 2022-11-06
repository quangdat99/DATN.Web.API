namespace DATN.Web.Service.Contexts
{
    /// <summary>
    /// Phiên làm việc
    /// </summary>
    public interface IContextService
    {
        /// <summary>
        /// Gán context
        /// </summary>
        void Set(ContextData contextData);

        /// <summary>
        /// Lấy context
        /// </summary>
        ContextData Get();
    }
}
