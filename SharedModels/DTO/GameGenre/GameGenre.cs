using System.Collections.Generic;

namespace SharedModels.DTO.GameGenre
{
	public class GameGenre
	{
		public GameGenre()
		{
			Genres = new List<string>();
		}

		public List<string> Genres { get; set; }
	}
}
