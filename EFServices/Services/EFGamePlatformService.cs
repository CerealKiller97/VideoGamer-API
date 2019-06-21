using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Domain;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTO.GamePlatform;

namespace EFServices.Services
{
	public class EFGamePlatformService : IGamePlatformService
	{
		private readonly VideoGamerDbContext _context;

		public EFGamePlatformService(VideoGamerDbContext context)
		{
			_context = context;
		}

		public async Task Add(int gameId, GamePlatform dto)
		{
			var game = await _context.Games.FindAsync(gameId);

			if (game == null)
			{
				throw new EntityNotFoundException("Game");
			}


			List<Platform> genres = new List<Platform>();

			foreach (var genre in dto.Platforms)
			{
				var platformCheck = _context.Platforms.Find(genre);
				if (platformCheck == null)
				{
					throw new EntityNotFoundException("Genre");
				} else
				{
					genres.Add(platformCheck);
				}
			}

			if (dto.Platforms.Count != genres.Count)
			{
				throw new Exception("One of genres doesn't exist.");
			}

			List<Domain.Relations.GamePlatform> pairs = new List<Domain.Relations.GamePlatform>();
			foreach (var platform in dto.Platforms)
			{
				pairs.Add(new Domain.Relations.GamePlatform
				{
					GameId = gameId,
					PlatformId = platform
				});
			}

			await _context.GamePlatforms.AddRangeAsync(pairs);

			await _context.SaveChangesAsync();
		}

		public async Task Delete(int gameId, GamePlatform dto)
		{
			var game = await _context
								.Games
								.Include(u => u.GameGenres)
								.FirstOrDefaultAsync(g => g.Id == gameId);

			if (game == null)
			{
				throw new EntityNotFoundException("Game");
			}


			foreach (int item in dto.Platforms)
			{
				var platforms = game.GamePlatforms.FirstOrDefault(g => g.PlatformId == item);

				game.GamePlatforms.Remove(platforms);

				await _context.SaveChangesAsync();
			}
		}
	}
}
