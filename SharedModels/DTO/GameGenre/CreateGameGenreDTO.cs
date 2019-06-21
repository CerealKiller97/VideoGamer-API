using System.Collections.Generic;

namespace SharedModels.DTO.GameGenre
{
	public class CreateGameGenreDTO
	{
		public CreateGameGenreDTO()
		{
			Genres = new List<int>();
		}

		public List<int> Genres { get; set; }
	}
}
