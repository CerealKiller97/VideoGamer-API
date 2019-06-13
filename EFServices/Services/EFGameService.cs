using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTO.Game;
using System.Threading.Tasks;

namespace EFServices.Services
{
    public class EFGameService: BaseService<Game, GameSearchRequest>, IGameService
    {
        public EFGameService(VideoGamerDbContext context) : base(context)
        {
        }

        public async Task<PagedResponse<Game>> All(GameSearchRequest request)
        {
            // var query = _context.Games.AsQueryable();
            // BuildingQuery(query, request)
            return null;
        }

        public async Task<int> Count() => await _context.Games.CountAsync();

        public async Task Create(CreateGameDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(int id)
        {
            var game = await _context.Games.FindAsync(id);


            if (game is null)
            {
                throw new EntityNotFoundException("Game");
            }

            _context.Games.Remove(game);
            _context.SaveChanges();

        }

        public async Task<Game> Find(int id)
        {
            var game = await _context.Games
                                .Include(g => g.GamePlatforms)
                                .Include(g => g.Publisher)

                                .FirstOrDefaultAsync(g => g.Id == id);

            if (game is null)
            {
                throw new EntityNotFoundException("Game");
            }

            return new Game
            {
                Id = game.Id,
                AgeLabel = game.AgeLabel,
                DeveloperId = game.DeveloperId,
                Engine = game.Engine,
                GameMode = game.GameMode,
                Name = game.Name,
                Publisher = new SharedModels.DTO.Publisher
                {
                    Name = game.Publisher.Name,
                    Founded = game.Publisher.Founded,
                    Website = game.Publisher.Website,
                    HQ = game.Publisher.HQ,
                    ISIN = game.Publisher.ISIN
                },
                PublisherId = game.PublisherId
            };
        }

        public async Task Update(int id, CreateGameDTO dto)
        {
            throw new System.NotImplementedException();
        }

    }
}