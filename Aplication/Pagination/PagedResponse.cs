using System.Collections.Generic;
using Domain;
using SharedModels.DTO;

namespace Aplication.Pagination
{
    public class PagedResponse<T> where T : BaseDTO
    {
        public IEnumerable<T> Data { get; set; }
        public int Total { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PerPage { get; set; } = 10;
    }
}
