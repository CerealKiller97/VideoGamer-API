using SharedModels.DTO.GamePlatform;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
	public interface IGamePlatformService
	{
		Task Add(int gameId, GamePlatform dto);
		Task Delete(int gameId, GamePlatform dto);
	}
}
