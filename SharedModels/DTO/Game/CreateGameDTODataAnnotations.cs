using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.DTO.Game
{
	public class CreateGameDTODataAnnotations
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Engine { get; set; }
		[Required]
		public int PublisherId { get; set; }
		[Required]
		public PegiAgeRating AgeLabel { get; set; }
		[Required]
		public DateTime ReleaseDate { get; set; }
		[Required]
		public int UserId { get; set; } = 4;
		[Required]
		public GameModes GameMode { get; set; }
		[Required]
		public int DeveloperId { get; set; }
		[Required]
		public List<int> Genres = new List<int>();
		[Required]
		public List<int> Platforms = new List<int>();
		[Required]
		public IFormFile Path { get; set; }
	}
}
