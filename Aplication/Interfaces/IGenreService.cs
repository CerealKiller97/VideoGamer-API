using Aplication.Searches;
using SharedModels.DTO.Genre;

namespace Aplication.Interfaces
{
	public interface IGenreService : IService<Genre, CreateGenreDTO, GenreSearchRequest>
	{

	}
}
