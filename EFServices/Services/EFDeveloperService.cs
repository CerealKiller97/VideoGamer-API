using System.Linq;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using EntityConfiguration;
using FluentValidation;
using SharedModels.DTO;
using SharedModels.Fluent.Developer;

namespace EFServices.Services
{
    public class EFDeveloperService : BaseService<Developer, DeveloperSearchRequest>, IDeveloperService
    {
        public EFDeveloperService(VideoGamerDbContext context) : base(context)
        {
        }

        protected override IQueryable<Developer> BuildingQuery(IQueryable<Developer> query, DeveloperSearchRequest request)
        {
            return null;
            //if (request.Name != null)
            //{
            //    string keyword = request.Name.ToLower();
            //    query = query.Where(q => q.Name.ToLower().Contains(keyword));
            //}

            //if (request.Founded != null)
            //{
                
            //}

            //return null;
        }

        public PagedResponse<Developer> All(DeveloperSearchRequest request)
        {
            var query = _context.Developers.AsQueryable();
            // var buildedQuery = BuildingQuery(query, request);
            // GeneratePagedResponse(buildedQuery<Developer>, request);
            return null;
        }

        public Developer Find(object id)
        {
            var Developer = _context.Developers.Find(id);

            if (Developer is null)
            {
                throw new EntityNotFoundException("Developer");
            }

            return new Developer
            {
                Id =  Developer.Id,
                Name = Developer.Name,
                Founded = Developer.Founded,
                Website = Developer.Website,
                HQ = Developer.HQ
            };
        }

        public void Create(CreateDeveloperDTO dto)
        {
            var validator = new DeveloperFluentValidatior();
            var valid = validator.Validate(dto);

            if (!valid.IsValid)
            {
                throw new ValidationException(valid.Errors);
            }

            _context.Developers.Add(new Domain.Developer
            {
                 Name = dto.Name,
                 HQ = dto.HQ,
                 Founded = dto.Founded,
                 Website = dto.Website
            });

             _context.SaveChanges();

        }

        public void Update(CreateDeveloperDTO dto)
        {
            var validator = new DeveloperFluentValidatior();
            var errors = validator.Validate(dto);
        }

        public void Delete(object id)
        {
            var Developer = _context.Developers.Find(id);

            if (Developer is null)
            {
                throw new EntityNotFoundException("Developer");
            }

            _context.Remove(Developer);
            _context.SaveChanges();
        }

        public int Count() => _context.Developers.Count();

        
    }
}