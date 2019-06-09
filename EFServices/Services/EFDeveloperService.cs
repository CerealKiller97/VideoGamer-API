using System;
using System.Linq;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using EntityConfiguration;
using FluentValidation;
using SharedModels.DTO;
using SharedModels.Fluent.Developer;
using SharedModels.Formatters;

namespace EFServices.Services
{
    public class EFDeveloperService : BaseService<Developer, DeveloperSearchRequest>, IDeveloperService
    {
        public EFDeveloperService(VideoGamerDbContext context) : base(context)
        {
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
            //var validator = new DeveloperFluentValidatior();
            //var valid = validator.Validate(dto);

            //var errors = ValidationFormatter.Format(valid);

            //if (!valid.IsValid)
            //{
            //    throw new ValidationException(errors.ToString());
            //}

            _context.Developers.Add(new Domain.Developer
            {
                 Name = dto.Name,
                 HQ = dto.HQ,
                 Founded = (DateTime) dto.Founded,
                 Website = dto.Website
            });

             _context.SaveChanges();

        }

        public void Update(CreateDeveloperDTO dto)
        {
           
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