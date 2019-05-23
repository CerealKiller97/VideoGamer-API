using System;
using System.Collections.Generic;
using Domain.Relations;

namespace Domain
{
	public class Genre : AbstractModel
	{
		public string Name { get; set; }
		public ICollection<GameGenre> GameGenre { get; set; }
	}
}
