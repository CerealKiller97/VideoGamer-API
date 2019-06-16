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
    public class EFPublisherService : BaseService<Domain.Publisher, PublisherSearchRequest>, IPublisherService
    {
        public EFPublisherService(VideoGamerDbContext context) : base(context)
        {
        }

        public async Task<PagedResponse<SharedModels.DTO.Publisher>> All(PublisherSearchRequest request)
        {
            var query = _context.Publishers
				.Include(p => p.Games)
				.AsQueryable();

            var buildedQuery = BuildQuery(query, request);

            return buildedQuery.Select(p => new SharedModels.DTO.Publisher
			{
                Id = p.Id,
                Name = p.Name,
                ISIN = p.ISIN,
                Founded = p.Founded,
                HQ = p.HQ,
                Website = p.Website,
				Games = p.Games.Select(game => new SharedModels.DTO.Game.Game
				{
					Id = game.Id,
					Name = game.Name,
					Engine = game.Engine,
					DeveloperName= game.Developer.Name,
					PublisherName = game.Publisher.Name,
					GameMode = Enum.GetName(typeof(GameModes), game.GameMode).ToString(),
					AgeLabel = Enum.GetName(typeof(PegiAgeRating), game.AgeLabel).ToString()

				}).AsEnumerable()
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
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);

            if (publisher == null)
            {
                throw new EntityNotFoundException("Publisher");
            }

            _context.Remove(publisher);
            await _context.SaveChangesAsync();
        }

        public async Task<SharedModels.DTO.Publisher> Find(int id)
        {
			var dev = await _context.Publishers
				.Include(d => d.Games)
				.ThenInclude(g => g.Developer)
				.FirstOrDefaultAsync(d => d.Id == id);

			if (dev == null)
			{
				throw new EntityNotFoundException("publisher");
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

			return new SharedModels.DTO.Publisher
			{
				Id = dev.Id,
				Name = dev.Name,
				Website = dev.Website,
				Founded = dev.Founded,
				HQ = dev.HQ,
				Games = games
			};
		}

        public async Task Update(int id, CreatePublisherDTO dto)
        {
			var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);

			if (publisher == null)
			{
				throw new EntityNotFoundException("Publisher");
			}


			if (publisher.Name != dto.Name)
			{
				publisher.Name = dto.Name;
			}

			if (publisher.HQ != dto.Name)
			{
				publisher.HQ = dto.HQ;
			}

			if (publisher.Founded != dto.Founded)
			{
				publisher.Founded = (DateTime)dto.Founded;
			}

			if (publisher.Website != dto.Website)
			{
				publisher.Website = dto.Website;
			}

			_context.Entry(publisher).State = EntityState.Modified;

			await _context.SaveChangesAsync();

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
                string isin = request.ISIN.ToLower();
                query = query.Where(q => q.ISIN.ToLower().Contains(isin));
            }

            return query;
        }

    }
}
