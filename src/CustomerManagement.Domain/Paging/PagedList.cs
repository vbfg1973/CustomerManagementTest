using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Domain.Paging
{
    /// <summary>
    ///     Paged items object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T>
    {
        private const int MaxPageSize = 1000;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="count"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        public PagedList(List<T> items, int count, int pageNumber = 1, int pageSize = 25)
        {
            Data = new List<T>(items);
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
        }

        /// <summary>
        ///     Current page of results
        /// </summary>
        public int CurrentPage { get; }

        /// <summary>
        ///     All pages available
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        ///     Size of current page
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        ///     Total count of all items
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        ///     Is there a previous page
        /// </summary>
        public bool HasPrevious => CurrentPage > 1;

        /// <summary>
        ///     Is there a next page
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;

        /// <summary>
        ///     List of all data in current page
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        ///     Produce a paged list from a supplied IQueryable
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pagedQuery"></param>
        /// <returns></returns>
        public static async Task<PagedList<T>> ToPagedList<TU>(IQueryable<T> source, TU pagedQuery)
            where TU : BasePagedQuery
        {
            var pageSize = Math.Min(MaxPageSize, pagedQuery.PageSize);
            var count = source.Count();
            var items = await source.Skip((pagedQuery.PageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pagedQuery.PageNumber, pageSize);
        }
    }
}