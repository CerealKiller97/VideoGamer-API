using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
	public interface IGamePlatformService
	{
		Task Add(int gameId, IEnumerable<int> platforms);
		Task Delete(int gameId, IEnumerable<int> platforms);
	}
}
