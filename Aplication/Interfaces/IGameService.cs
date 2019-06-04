using SharedModels.DTO;
using SharedModels.Fluent.Game;

namespace Aplication.Interfaces
{
    public interface IGameService: IUserService<Game, GameFluentValidator, >
    {
        
    }
}