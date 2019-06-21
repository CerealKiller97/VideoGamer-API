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

		public EFGamePlatformService(VideoGamerDbContext context) => _context = context;

		public async Task Add(int gameId, GamePlatform dto)
		{
			var game = await _context.Games
									.Include(g => g.GamePlatforms)	
									.FirstOrDefaultAsync(g => g.Id == gameId);

			if (game == null)
			{
				throw new EntityNotFoundException("Game");
			}

			List<Platform> platforms = new List<Platform>();

			foreach (var platform in dto.Platforms)
			{
				Platform platformCheck = _context.Platforms.Find(platform);
				if (platformCheck == null)
				{
					throw new EntityNotFoundException("Platform");
				} else
				{
					bool contains = game.GamePlatforms.Any(g => g.PlatformId == platform);

					if (contains)
					{
						throw new DataAlreadyExistsException();
					}

					platforms.Add(platformCheck);
				}
			}

			if (dto.Platforms.Count != platforms.Count)
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
			var game = await _context.Games
									 .Include(u => u.GamePlatforms)
									 .FirstOrDefaultAsync(g => g.Id == gameId);

			if (game == null)
			{
				throw new EntityNotFoundException("Game");
			}

			List<Domain.Relations.GamePlatform> gamePlatforms = new List<Domain.Relations.GamePlatform>();

			foreach (int item in dto.Platforms)
			{
				var platform = game.GamePlatforms.FirstOrDefault(g => g.PlatformId == item);

				if (platform == null)
				{
					throw new EntityNotFoundException("Platform");
				}

				gamePlatforms.Add(platform);
			}

			if (gamePlatforms.Count != dto.Platforms.Count)
			{
				throw new Exception("One of platforms doens't exist.");
			}

			foreach (var platform in gamePlatforms)
			{
				_context.Remove(platform);
				await _context.SaveChangesAsync();
			}
		}
	}
}
