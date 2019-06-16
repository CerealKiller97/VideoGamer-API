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
using System.Collections.Generic;
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
                                    .AsQueryable();
            
            var buildedQuery = BuildQuery(query, request);

            return query.Select(game => new Game
			{
                Id = game.Id,
                Name = game.Name,
                Engine = game.Engine,
                PublisherName = game.Publisher.Name,
                AgeLabel = game.AgeLabel.ToString(),
                GameMode = game.GameMode.ToString(),
                ReleaseDate = game.ReleaseDate
            }).Paginate(request.PerPage, request.Page);

        }

        public async Task<int> Count() => await _context.Games.CountAsync();

        public async Task Create(CreateGameDTO dto)
        {

			string path = await _fileService.Upload(dto.Path);
			
			// VALIDATION FILE

			List<GamePlatform> gamePlatforms = new List<GamePlatform>();
			List<GameGenre> gameGenres = new List<GameGenre>();

			foreach (int platform in dto.Platforms)
			{
				gamePlatforms.Add(new GamePlatform
				{
					PlatformId = platform
				});
			}

			foreach (int genre in dto.Genres)
			{
				gameGenres.Add(new GameGenre
				{
					GenreId = genre
				});
			}

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
				GamePlatforms = gamePlatforms,
				GameGenres = gameGenres,
				Path = path
			};


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

        public async Task<SharedModels.DTO.Game.Game> Find(int id)
        {
            var game = await _context.Games
                                .Include(g => g.GamePlatforms)
                                .Include(g => g.Publisher)
                                .FirstOrDefaultAsync(g => g.Id == id);

            if (game is null)
            {
                throw new EntityNotFoundException("Game");
            }

            return new SharedModels.DTO.Game.Game
            {
                Id = game.Id,
                AgeLabel = game.AgeLabel.ToString(),
                Engine = game.Engine,
                GameMode = game.GameMode.ToString(),
                Name = game.Name,
                PublisherName = game.Publisher.Name
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
                string publisher = request.Developer.ToLower();
                query = query.Where(q => q.Developer.Name.ToLower().Contains(publisher));
            }

            if (request.Engine != null)
            {
                string engine = request.Engine.ToLower();
                query = query.Where(q => q.Engine.ToLower().Contains(engine));
            }

            if (request.AgeLabel != null)
            {
                query = query.Where(q => q.AgeLabel == request.AgeLabel);
            }

            if (request.GameMode != null)
            {
                query = query.Where(q => q.GameMode == request.GameMode);
            }

            return query;
        }
    }
}