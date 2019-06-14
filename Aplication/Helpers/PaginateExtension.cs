using Aplication.Pagination;
using System;
using System.Linq;

namespace Aplication.Helpers
{
    namespace MyComicList.Application.Helpers
    {
        public static class PaginateExtension
        {
            public static PagedResponse<T> Paginate<T>(this IQueryable<T> query, int perPage, int currentPage)
            {
                var result = query
                                .Skip((currentPage - 1) * perPage)
                                .Take(perPage);

                int totalCount = query.Count();
                int pagesCount = (int) Math.Ceiling((double) totalCount / perPage);

                return new PagedResponse<T>()
                {
                    Page = currentPage,
                    PagesCount = pagesCount,
                    PerPage = perPage,
                    Total = totalCount,
                    Data = result.ToList()
                };
            }
        }
    }
}
