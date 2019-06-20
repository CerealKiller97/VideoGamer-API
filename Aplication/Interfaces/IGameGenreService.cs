using SharedModels.DTO.GameGenre;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
	public interface IGameGenreService
	{
		Task AddGenreToGame(int gameId, CreateGameGenreDTO dto);
		Task RemoveGenreFrom(int gameId, DeleteGameGenreDTO dto);
	}
}
