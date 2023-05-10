namespace CustomerManagement.Domain.Paging
{
    /// <summary>
    ///     Base class for paged queries
    /// </summary>
    public abstract class BasePagedQuery
    {
        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        protected BasePagedQuery(int? pageSize, int? pageNumber)
        {
            PageSize = pageSize ?? 25;
            PageNumber = pageNumber ?? 1;
        }

        /// <summary>
        ///     Size of requested page
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        ///     Requested page number
        /// </summary>
        public int PageNumber { get; }
    }
}