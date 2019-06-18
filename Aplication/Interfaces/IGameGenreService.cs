using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
	public interface IGameGenreService
	{
		Task AddGenreToGame(int gameId, IEnumerable<int> genres);
		Task RemoveGenreFrom(int gameId, IEnumerable<int> genres);
	}
}
