using System;

namespace Domain.Relations
{
	public class GameGenre<TGameKey, TGenreKey>
	{
		public TGenreKey GenreId { get; set; }
		public Genre Genre { get; set; }

		public TGameKey GameId { get; set; }
		public Game<Guid> Game { get; set; }
	}
}
