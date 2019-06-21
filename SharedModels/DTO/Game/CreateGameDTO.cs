using Domain;
using System;

namespace SharedModels.DTO.Game
{
	public class CreateGameDTO
    {
        public string Name { get; set; }
        public string Engine { get; set; }
        public int PublisherId { get; set; }
        public PegiAgeRating AgeLabel { get; set; }
        public DateTime ReleaseDate { get; set; }
		public int UserId { get; set; }
        public GameModes GameMode { get; set; }
        public int DeveloperId { get; set; }
        public string Path { get; set; }
		public string FilePath { get; set; }
	}
}
