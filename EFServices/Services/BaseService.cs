using Aplication.Searches;
using EntityConfiguration;
using System.Linq;
using Domain;

namespace EFServices
{
    abstract public class BaseService<T, TSearch> where T : AbstractModel where TSearch : BaseSearchRequest
    {
        protected readonly VideoGamerDbContext _context;
        public BaseService(VideoGamerDbContext context) => _context = context;

        protected abstract IQueryable<T> BuildQuery(IQueryable<T> query, TSearch request);
    }
}
