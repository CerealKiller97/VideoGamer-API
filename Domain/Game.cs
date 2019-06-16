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
    public class Game : AbstractModel
    {
        public string Name { get; set; }
        public string Engine { get; set; }
        public int? PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public PegiAgeRating AgeLabel { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<GameGenre> GameGenres { get; set; }
        public ICollection<GamePlatform> GamePlatforms { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public GameModes GameMode { get; set; }
        public int? DeveloperId { get; set; }
        public Developer Developer { get; set; }
        public string Path { get; set; }
	}
}
