using System.Collections.Generic;
using Domain;

namespace Aplication.Pagination
{
    public class PagedResponse<T> where T : AbstractModel
    {
        public IEnumerable<T> Data { get; set; }
        public int Total { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PerPage { get; set; } = 10;
    }
}
