using System;
using System.Collections.Generic;
using Domain.Relations;

namespace Domain
{
	public enum PegiAgeRating
	{
		Pegi3 = 3,
		Pegi7 = 7,
		Pegi12 = 12,
		Pegi16 = 16,
		Pegi18 = 18
	}
	public class Game<TUserKey> : AbstractModel<Guid>
	{
		public string Name { get; set; }
		public string Engine { get; set; }
		public int PublisherId { get; set; }
		public Publisher Publisher { get; set; }
		public PegiAgeRating AgeLabel { get; set; }
		public DateTime ReleaseDate { get; set; }
		public ICollection<Genre> Genres { get; set; }
		public ICollection<GamePlatform<Guid,Guid>> GamePlatforms { get; set; }
        public TUserKey UserId { get; set; }
        public User User { get; set; }
	}
}
