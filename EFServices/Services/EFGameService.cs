using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using AutoMapper;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTO.Game;
using System.Linq;

namespace EFServices.Services
{
    public class EFGameService: BaseService<Game, GameSearchRequest>, IGameService
    {
        public EFGameService(VideoGamerDbContext context) : base(context)
        {
        }

        public PagedResponse<Game> All(GameSearchRequest request)
        {
            // var query = _context.Games.AsQueryable();
            // BuildingQuery(query, request)
            return null;
        }

        public int Count() => _context.Games.Count();

        public void Create(CreateGameDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(object id)
        {
            var game = _context.Games.Find(id);

            if (game is null)
            {
                throw new EntityNotFoundException("Game");
            }

            _context.Games.Remove(game);
            _context.SaveChanges();

        }

        public Game Find(object id)
        {
            var game = _context.Games
                                .Include(g => g.GamePlatforms);

            if (game is null)
            {
                throw new EntityNotFoundException("Game");
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Domain.Game, Game>());

            var gameDTO = Mapper.Map<Game>(game);

            return gameDTO;
        }

        public void Update(object id, CreateGameDTO dto)
        {
            throw new System.NotImplementedException();
        }

    }
}