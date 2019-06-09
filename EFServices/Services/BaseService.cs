using Aplication.Pagination;
using Aplication.Searches;
using EntityConfiguration;
using System.Linq;
using SharedModels.DTO;

namespace EFServices
{
    abstract public class BaseService<T, TSearch> where T : BaseDTO
    {
        protected readonly VideoGamerDbContext _context;
        public BaseService(VideoGamerDbContext context) => _context = context;

        protected PagedResponse<T> GeneratePagedResponse(IQueryable<T> query, BaseSearchRequest request)
        {
            if (request.PerPage != null)
            {
                query = query.Take(request.PerPage.Value);
            }

            //.Skip((pageNumber - 1) * perPage)
            //return new PagedResponse<T>();
            return null;
        }
    }
}
