using System.Collections.Generic;

namespace Aplication.Pagination
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Total { get; set; }
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 10;
        public int PagesCount { get; set; }
    }
}
