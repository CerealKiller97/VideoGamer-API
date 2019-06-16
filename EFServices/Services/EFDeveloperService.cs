using System;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Helpers.MyComicList.Application.Helpers;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using Domain;
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

        public async Task<PagedResponse<SharedModels.DTO.Developer>> All(DeveloperSearchRequest request)
        {
            var query = _context.Developers
                .Include(d => d.Games)
                .AsQueryable();

            var buildedQuery = BuildQuery(query, request);

            //TODO: AutoMapper

            return buildedQuery.Select(dev => new SharedModels.DTO.Developer
            {
                Id = dev.Id,
                Name = dev.Name,
                Founded = dev.Founded,
                HQ = dev.HQ,
                Website = dev.Website,
                Games = dev.Games.Select(game => new SharedModels.DTO.Game.Game
                {
                    Id = game.Id,
                    Name = game.Name,
                    Engine = game.Engine,
                    DeveloperName = game.Developer.Name,
                    PublisherName = game.Publisher.Name,
                    GameMode = Enum.GetName(typeof(GameModes), game.GameMode).ToString(),
                    AgeLabel = Enum.GetName(typeof(PegiAgeRating), game.AgeLabel).ToString()

                }).AsEnumerable()
            }).Paginate(request.PerPage, request.Page);
        }

        public async Task<SharedModels.DTO.Developer> Find(int id)
        {
            var dev = await _context.Developers
                .Include(d => d.Games)
                .ThenInclude(g => g.Publisher)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dev == null)
            {
                throw new EntityNotFoundException("Developer");
            }

            var games = dev.Games.Select(game => {
                var gameMode = Enum.GetName(typeof(GameModes), game.GameMode);
                var ageLabel = Enum.GetName(typeof(PegiAgeRating), game.AgeLabel);

                return new SharedModels.DTO.Game.Game
                {
                    Id = game.Id,
                    Name = game.Name,
                    Engine = game.Engine,
                    PublisherName = game.Publisher.Name,
                    DeveloperName = game.Developer.Name,
                    GameMode = gameMode.ToString(),
                    AgeLabel = ageLabel.ToString()
                };
            });

            return new SharedModels.DTO.Developer
            {
                Id = dev.Id,
                Name = dev.Name,
                Website = dev.Website,
                Founded = dev.Founded,
                HQ = dev.HQ,
                Games = games
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

            if (developer.Name != dto.Name)
            {
                developer.Name = dto.Name;
            }

            if (developer.HQ != dto.Name)
            {
                developer.HQ = dto.HQ;
            }

            if (developer.Founded != dto.Founded)
            {
                developer.Founded = (DateTime) dto.Founded;
            }

            if (developer.Website != dto.Website)
            {
                developer.Website = dto.Website;
            }

            // developer.UpdatedAt = DateTime.Now;

			_context.Entry<Domain.Developer>(developer).State = EntityState.Modified;


			//_context.Developers.Update(developer);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Developer = await _context.Developers.FindAsync(id);

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