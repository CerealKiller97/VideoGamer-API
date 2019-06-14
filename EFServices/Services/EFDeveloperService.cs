using System;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Helpers.MyComicList.Application.Helpers;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using AutoMapper;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTO;

namespace EFServices.Services
{
    public class EFDeveloperService :
        BaseService<Domain.Developer, DeveloperSearchRequest>, IDeveloperService
    {
        public EFDeveloperService(VideoGamerDbContext context) : base(context)
        {
        }

        public async Task<PagedResponse<Developer>> All(DeveloperSearchRequest request)
        {
            var query = _context.Developers
                .AsQueryable();

            var buildedQuery = BuildQuery(query, request);

            //TODO: AutoMapper

            return buildedQuery.Select(dev => new Developer
            {
                Id = dev.Id,
                Name = dev.Name,
                Founded = dev.Founded,
                HQ = dev.HQ,
                Website = dev.Website
            }).Paginate(request.PerPage, request.Page);
        }

        public async Task<Developer> Find(int id)
        {
            var dev = await _context.Developers
                .Include(d => d.Games)
                .FirstOrDefaultAsync(d => d.Id == id) ?? null;

            if (dev == null)
            {
                throw new EntityNotFoundException("Developer");
            }

            return new Developer
            {
                Id = dev.Id,
                Name = dev.Name,
                Website = dev.Website,
                Founded = dev.Founded,
                HQ = dev.HQ
            };
        }

        public async Task Create(CreateDeveloperDTO dto)
        {
            await _context.Developers.AddAsync(new Domain.Developer
            {
                Name = dto.Name,
                HQ = dto.HQ,
                Founded = (DateTime) dto.Founded,
                Website = dto.Website
            });

            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, CreateDeveloperDTO dto)
        {
            var developer = await _context.Developers.FindAsync(id);

            if (developer == null)
            {
                throw new EntityNotFoundException("Developer");
            }

            if (dto.Name != null)
            {
                developer.Name = dto.Name;
            }

            if (dto.HQ != null)
            {
                developer.HQ = dto.HQ;
            }

            if (dto.Founded != null)
            {
                developer.Founded = (DateTime) dto.Founded;
            }

            if (dto.Website != null)
            {
                developer.Website = dto.Website;
            }

            developer.UpdatedAt = DateTime.Now;

            // _context.Developers.Update(developer);

            // _context.Entry<Domain.Developer>(developer).State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {

            var Developer = await _context.Developers.FindAsync(id) ?? null;

            if (Developer == null)
            {
                throw new EntityNotFoundException("Developer");
            }

            _context.Remove(Developer);
            await _context.SaveChangesAsync();
        }

        public async Task<int> Count() => await _context.Developers.CountAsync();

        protected override IQueryable<Domain.Developer> BuildQuery(IQueryable<Domain.Developer> query, DeveloperSearchRequest request)
        {
            if (request.Name != null)
            {
                string keyword = request.Name.ToLower();
                query = query.Where(q => q.Name.ToLower().Contains(keyword));
            }

            return query;
        }
    }
}