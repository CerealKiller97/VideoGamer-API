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
		public string PublisherName { get; set; }
        public string DeveloperName { get; set; }
        public string AgeLabel { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string GameMode { get; set; }
        IEnumerable<Genre.Genre> Genres = new List<Genre.Genre>();
	}
}
