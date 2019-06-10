using Aplication.Searches;
using SharedModels.DTO.Game;

namespace Aplication.Interfaces
{
    public interface IGameService : IService<Game, CreateGameDTO, GameSearchRequest>
    {
        
    }
}