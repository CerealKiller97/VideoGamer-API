using Aplication.Exceptions;
using Aplication.FileUpload;
using Aplication.Helpers.MyComicList.Application.Helpers;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using Domain;
using Domain.Relations;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTO.Game;
using System;
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

		public async Task<PagedResponse<SharedModels.DTO.Game.Game>> All(GameSearchRequest request) 
        {
            var query = _context.Games
                                .Include(g => g.Publisher)
                                .Include(g => g.Developer)
                                .Include(g => g.GameGenres)
								.Include(g => g.GamePlatforms)
                                .AsQueryable();
            
            var buildedQuery = BuildQuery(query, request);

			return buildedQuery.Select(game => new SharedModels.DTO.Game.Game
			{
                Id = game.Id,
                Name = game.Name,
                Engine = game.Engine,
                PublisherName = game.Publisher.Name,
				DeveloperName = game.Developer.Name,
                AgeLabel = game.AgeLabel.ToString(),
                GameMode = game.GameMode.ToString(),
                ReleaseDate = game.ReleaseDate,
				ImagePath = game.Path,
				Genres = game.GameGenres.Select(g => g.Genre.Name).ToList(),
				Platforms =  game.GamePlatforms.Select(gg => Enum.GetName(typeof(Platforms), gg.Platform.Name)).ToList()
			}).Paginate(request.PerPage, request.Page);
        }

        public async Task<int> Count() => await _context.Games.CountAsync();

        public async Task Create(CreateGameDTO dto)
        {
			var (Server, FilePath) = await _fileService.Upload(dto.Path);

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
				Path = Server,
				FullPath = FilePath
			};

			await _context.Games.AddAsync(game);

			await _context.SaveChangesAsync();

			//var game = new Domain.Game
			//{
			//	Name = dto.Name,
			//	Engine = dto.Engine,
			//	DeveloperId = dto.DeveloperId,
			//	PublisherId = dto.PublisherId,
			//	ReleaseDate = dto.ReleaseDate,
			//	UserId = dto.UserId,
			//	GameMode = dto.GameMode,
			//	AgeLabel = dto.AgeLabel,
			//	Path = dto.Path,
			//	FullPath = dto.FilePath
			//};

			//await _context.Games.AddAsync(game);

			//         await _context.SaveChangesAsync();
		}

        public async Task Delete(int id)
        {
            var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);


            if (game == null)
            {
                throw new EntityNotFoundException("Game");
            }

            _context.Games.Remove(game);

			await _fileService.Remove(game.FullPath);

			await _context.SaveChangesAsync();
        }

        public async Task<SharedModels.DTO.Game.Game> Find(int id)
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

			var genres = game.GameGenres.Select(g => g.GenreId).ToList();

			List<string> gameGenres = new List<string>();

			foreach (var item in genres)
			{
				var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == item);

				if (genre != null)
				{
					gameGenres.Add(genre.Name);
				}
			}


			var platforms = game.GamePlatforms.Select(gp => gp.PlatformId).ToList();

			List<string> gamePlatforms = new List<string>();

			foreach (var item in platforms)
			{
				var platform = await _context.Platforms.FirstOrDefaultAsync(g => g.Id == item);

				if (platform != null)
				{
					var platformName = Enum.GetName(typeof(Platforms), platform.Name);
					gamePlatforms.Add(platformName);
				}
			}

			return new SharedModels.DTO.Game.Game
			{
                Id = game.Id,
                AgeLabel = game.AgeLabel.ToString(),
                Engine = game.Engine,
                GameMode = game.GameMode.ToString(),
                Name = game.Name,
                PublisherName = game.Publisher.Name,
				DeveloperName = game.Developer.Name,
				ImagePath = game.Path,
				ReleaseDate = game.ReleaseDate,
				Genres = gameGenres,
				Platforms = gamePlatforms
			};
        }

        public async Task Update(int id, CreateGameDTO dto)
        {
			var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

			if (game == null)
			{
				throw new EntityNotFoundException("Game");
			}

			// Check if user sent picture, if send that means he is changing cover image

			if (dto.Path != null)
			{
				var (Server, FilePath) = await _fileService.Upload(dto.Path);
				//Remove previous image in case if user uploaded new image
				await _fileService.Remove(game.FullPath);
				game.Path = Server;
				game.FullPath = FilePath;
			}

			if (game.Name != dto.Name)
			{
				game.Name = dto.Name;
			}

			if (game.Engine != dto.Engine)
			{
				game.Engine = dto.Engine;
			}

			if (game.DeveloperId != dto.DeveloperId) 
			{
				game.DeveloperId = dto.DeveloperId;
			}

			if (game.PublisherId != dto.PublisherId)
			{
				game.PublisherId = dto.PublisherId;
			}

			if (game.AgeLabel != dto.AgeLabel)
			{
				game.AgeLabel = dto.AgeLabel;
			}

			if (game.ReleaseDate != dto.ReleaseDate)
			{
				game.ReleaseDate = dto.ReleaseDate;
			}

			if (dto.UserId == null)
			{
				game.UserId = game.UserId;
			}

			if (game.GameMode != dto.GameMode)
			{
				game.GameMode = dto.GameMode;
			}

			if (game.DeveloperId != dto.DeveloperId)
			{
				game.DeveloperId = dto.DeveloperId;
			}

			_context.Entry(game).State = EntityState.Modified;

			await _context.SaveChangesAsync();
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

			// TODO: Convert string to Enum and then check type and give back a response

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
