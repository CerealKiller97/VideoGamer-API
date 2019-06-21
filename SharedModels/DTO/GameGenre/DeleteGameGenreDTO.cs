using System.Collections.Generic;

namespace SharedModels.DTO.GameGenre
{
	public class DeleteGameGenreDTO
	{
		public DeleteGameGenreDTO()
		{
			Genres = new List<int>();
		}

		public List<int> Genres { get; set; }
	}
}
