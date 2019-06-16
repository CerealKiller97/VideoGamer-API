using System;
using System.Collections.Generic;

namespace SharedModels.DTO
{
	public class Publisher 
	{
        public int Id { get; set; }
		public string Name { get; set; }
		public string HQ { get; set; }
		public string ISIN { get; set; }
		public DateTime Founded { get; set; }
		public string Website { get; set; }
		public IEnumerable<Game.Game> Games = new List<Game.Game>();
	}
}
