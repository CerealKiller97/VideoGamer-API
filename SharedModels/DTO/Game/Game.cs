using System;
using System.Collections.Generic;
using Domain;

namespace SharedModels.DTO.Game
{
	public class Game : BaseDTO
	{
		public int Id { get; set; }		
		public string Name { get; set; }
		public string Engine { get; set; }
		public int? PublisherId { get; set; }
		public Publisher Publisher { get; set; }
		public PegiAgeRating AgeLabel { get; set; }
		public DateTime ReleaseDate { get; set; }
		public int UserId { get; set; }
		public GameModes GameMode { get; set; }
		public int? DeveloperId { get; set; }
        // IEnumerable<Genre> Genres { get; set; }
	}
}
