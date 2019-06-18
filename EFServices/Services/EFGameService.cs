using Aplication.Exceptions;
using Aplication.FileUpload;
using Aplication.Helpers.MyComicList.Application.Helpers;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using Domain.Relations;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTO.Game;
using System.Linq;
using System.Threading.Tasks;

namespace EFServices.Services
{
	public class EFGameService : BaseService<Domain.Game, GameSearchRequest>, IGameService
    {
		private readonly IFileService _fileService;
		public EFGameService(VideoGamerDbContext context, IFileService fileService) : base(context)
        {
			_fileService = fileService;

		}

		public async Task<PagedResponse<Game>> All(GameSearchRequest request) 
        {
            var query = _context.Games
                                .Include(g => g.Publisher)
                                .Include(g => g.Developer)
                                .Include(g => g.GameGenres)
								.Include(g => g.GamePlatforms)
                                .AsQueryable();
            
            var buildedQuery = BuildQuery(query, request);

            return buildedQuery.Select(game => new Game
			{
                Id = game.Id,
                Name = game.Name,
                Engine = game.Engine,
                PublisherName = game.Publisher.Name,
				DeveloperName = game.Developer.Name,
                AgeLabel = game.AgeLabel.ToString(),
                GameMode = game.GameMode.ToString(),
                ReleaseDate = game.ReleaseDate,
				ImagePath = game.Path
            }).Paginate(request.PerPage, request.Page);

        }

        public async Task<int> Count() => await _context.Games.CountAsync();

        public async Task Create(CreateGameDTO dto)
        {
			string path = await _fileService.Upload(dto.Path);
			
			// VALIDATION FILE

			var game = new Domain.Game
			{
				Name = dto.Name,
				Engine = dto.Engine,
				DeveloperId = dto.DeveloperId,
				PublisherId = dto.PublisherId,
				ReleaseDate = dto.ReleaseDate,
				UserId = dto.UserId,
				GameMode = dto.GameMode,
				AgeLabel = dto.AgeLabel,
				Path = path
			};
			
			foreach (int platform in dto.Platforms)
			{
				game.GamePlatforms.Add(new GamePlatform
				{
					PlatformId = platform
				});
			}

			foreach (int genre in dto.Genres)
			{
				game.GameGenres.Add(new GameGenre
				{
					GenreId = genre
				});
			}

			await _context.Games.AddAsync(game);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                throw new EntityNotFoundException("Game");
            }

            _context.Games.Remove(game);
            _context.SaveChanges();
        }

        public async Task<Game> Find(int id)
        {
            var game = await _context.Games
								.Include(g => g.Publisher)
								.Include(g => g.Developer)
								.Include(g => g.GameGenres)
								.Include(g => g.GamePlatforms)
                                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                throw new EntityNotFoundException("Game");
            }

            return new Game
			{
                Id = game.Id,
                AgeLabel = game.AgeLabel.ToString(),
                Engine = game.Engine,
                GameMode = game.GameMode.ToString(),
                Name = game.Name,
                PublisherName = game.Publisher.Name,
				DeveloperName = game.Developer.Name,
				ImagePath = game.Path,
				ReleaseDate = game.ReleaseDate
            };
        }

        public async Task Update(int id, CreateGameDTO dto)
        {
            
        }


        protected override IQueryable<Domain.Game> BuildQuery(IQueryable<Domain.Game> query, GameSearchRequest request)
        {
            if (request.Name != null)
            {
                string name = request.Name.ToLower();
                query = query.Where(q => q.Name.ToLower().Contains(name));
            }

            if (request.Publisher != null)
            {
                string publisher = request.Publisher.ToLower();
                query = query.Where(q => q.Publisher.Name.ToLower().Contains(publisher));
            }

			if (request.Developer != null)
			{
				string developer = request.Developer.ToLower();
				query = query.Where(q => q.Developer.Name.ToLower().Contains(developer));
			}

            if (request.Engine != null)
            {
                string engine = request.Engine.ToLower();
                query = query.Where(q => q.Engine.ToLower().Contains(engine));
            }

			// TODO: Convert string to Enum and then check type and gve back a response

			//if (request.AgeLabel != null) 
			//{
			//	query = query.Where(q => q.AgeLabel == request.AgeLabel);
			//}

			//if (request.GameMode != null)
			//{
			//	query = query.Where(q => q.GameMode == request.GameMode);
			//}

			return query;
        }
    }
}
