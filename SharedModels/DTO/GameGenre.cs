using System.Collections.Generic;

namespace SharedModels.DTO
{
	public class GameGenre
	{
		public int GameId { get; set; }
		public List<int> Genres = new List<int>();
	}
}
