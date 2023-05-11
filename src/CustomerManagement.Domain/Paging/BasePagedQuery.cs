namespace CustomerManagement.Domain.Paging
{
    /// <summary>
    ///     Base class for paged queries
    /// </summary>
    public abstract class BasePagedQuery
    {
        /// <summary>
        ///     Size of requested page
        /// </summary>
        public int PageSize { get; init; } = 25;

        /// <summary>
        ///     Requested page number
        /// </summary>
        public int PageNumber { get; init; } = 1;
    }
}