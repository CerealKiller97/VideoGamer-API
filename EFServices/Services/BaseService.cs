using Aplication.Pagination;
using Aplication.Searches;
using EntityConfiguration;
using System.Linq;

namespace EFServices
{
    abstract public class BaseService<T> where T : class
    {
        protected readonly VideoGamerDbContext _context;
        public BaseService(VideoGamerDbContext context) => _context = context;

        protected PagedResponse<T> GeneratePagedResponse(IQueryable query, BaseSearchRequest request)
        {
            if (request.PerPage != null)
            {
                query = query.Take(request.PerPage);
            }

            if (request.Name != null)
            {
                var keyword = request.Name.ToLower();
                query = query.Where(q => q.Name.ToLower().Contains(keyword));
            }

            //.Skip((pageNumber - 1) * perPage)
            //return new PagedResponse<T>();
            return null;
        }
        abstract protected void BuildingQuery(BaseSearchRequest request);
        abstract protected void EntityDatabaseAvailability(); 
    }
}
