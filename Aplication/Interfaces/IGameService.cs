using Aplication.Searches;
using SharedModels.DTO;
using SharedModels.Fluent.Game;

namespace Aplication.Interfaces
{
    public interface IGameService: IService<Game, GameFluentValidator,GameFluentValidator, GameSearchRequest> where Game : BaseDTO
    {
        
    }
}