using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace SharedModels.DTO.Game
{
    public class CreateGameDTO
    {
        public string Name { get; set; }
        public string Engine { get; set; }
        public int PublisherId { get; set; }
        public PegiAgeRating AgeLabel { get; set; }
        public DateTime ReleaseDate { get; set; }
		public int UserId { get; set; } = 4;
        public GameModes GameMode { get; set; }
        public int DeveloperId { get; set; }
        public List<int> Genres = new List<int>() { 1,3 };
		public List<int> Platforms = new List<int>() { 1, 3 };
        public IFormFile Path { get; set; }
	}
}
