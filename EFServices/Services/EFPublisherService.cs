using System.Linq;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Helpers.MyComicList.Application.Helpers;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTO;

namespace EFServices.Services
{
    public class EFPublisherService : BaseService<Domain.Publisher, PublisherSearchRequest>, IPublisherService
    {
        public EFPublisherService(VideoGamerDbContext context) : base(context)
        {
        }

        public async Task<PagedResponse<Publisher>> All(PublisherSearchRequest request)
        {
            var query = _context.Publishers.AsQueryable();

            var buildedQuery = BuildQuery(query, request);

            return buildedQuery.Select(p => new Publisher
            {
                Id = p.Id,
                Name = p.Name,
                ISIN = p.ISIN,
                Founded = p.Founded,
                HQ = p.HQ,
                Website = p.Website
            }).Paginate(request.PerPage, request.Page);
        }

        public async Task<int> Count() => await _context.Publishers.CountAsync();

        public async Task Create(CreatePublisherDTO dto)
        {
            await _context.Publishers.AddAsync(new Domain.Publisher
            {
                Name = dto.Name,
                ISIN = dto.ISIN,
                Founded = dto.Founded,
                HQ = dto.HQ,
                Website = dto.Website
            });

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync();

            if (publisher == null)
            {
                throw new EntityNotFoundException("Publisher");
            }

            _context.Remove(publisher);
            await _context.SaveChangesAsync();
        }

        public async Task<Publisher> Find(int id)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync();

            if (publisher == null)
            {
                throw new EntityNotFoundException("Publisher");
            }

            return new Publisher
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Founded = publisher.Founded,
                HQ = publisher.HQ,
                ISIN = publisher.ISIN,
                Website = publisher.Website
            };
        }

        public async Task Update(int id, CreatePublisherDTO dto)
        {
            throw new System.NotImplementedException();
        }

        protected override IQueryable<Domain.Publisher> BuildQuery(IQueryable<Domain.Publisher> query, PublisherSearchRequest request)
        {
            if (request.Name != null)
            {
                string name = request.Name.ToLower();
                query = query.Where(q => q.Name.ToLower().Contains(name));
            }

            if (request.ISIN != null)
            {
                string isin = request.Name.ToLower();
                query = query.Where(q => q.ISIN.ToLower().Contains(isin));
            }

            return query;
        }

    }
}
