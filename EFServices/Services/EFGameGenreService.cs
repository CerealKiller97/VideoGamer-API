using System.Collections.Generic;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Relations;
using EntityConfiguration;

namespace EFServices.Services
{
	public class EFGameGenreService : IGameGenreService
	{
		private readonly VideoGamerDbContext _context;

		public EFGameGenreService(VideoGamerDbContext context)
		{
			_context = context;
		}

		public async Task AddGenreToGame(int gameId, IEnumerable<int> genres)
		{
			List<GameGenre> pairs = new List<GameGenre>();
			foreach(var genre in genres)
			{
				pairs.Add(new GameGenre
				{
					GameId = gameId,
					GenreId = genre
				});
			}

			await _context.GameGenres.AddRangeAsync(pairs);

			await _context.SaveChangesAsync();
		}


		public async Task RemoveGenreFrom(int gameId, IEnumerable<int> genres)
		{
			
		}
	}
}
