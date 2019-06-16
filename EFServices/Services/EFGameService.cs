using Aplication.Exceptions;
using Aplication.Helpers.MyComicList.Application.Helpers;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using Domain;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTO.Game;
using System.Linq;
using System.Threading.Tasks;

namespace EFServices.Services
{
    public class EFGameService : BaseService<Domain.Game, GameSearchRequest>, IGameService
    {
        public EFGameService(VideoGamerDbContext context) : base(context)
        {
        }

        public async Task<PagedResponse<SharedModels.DTO.Game.Game>> All(GameSearchRequest request) 
        {
            var query = _context.Games
                                    .Include(g => g.Publisher)
                                    .Include(g => g.Developer)
                                    .Include(g => g.GameGenres)
                                    .AsQueryable();
            
            var buildedQuery = BuildQuery(query, request);

            return query.Select(game => new SharedModels.DTO.Game.Game              {
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
            await _context.AddAsync(new Domain.Game
            {
                
            });

            await _context.SaveChangesAsync();
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