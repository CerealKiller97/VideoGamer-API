using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.DTO.Game
{
	public class CreateGameDTODataAnnotations
	{
		[Required(ErrorMessage = "Name is required.")]
		[MinLength(5, ErrorMessage = "Name must be at least 5 characters long.")]
		[MaxLength(255, ErrorMessage = "Name can't be longer than 255 characters.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Engine is required.")]
		[MinLength(5, ErrorMessage = "Engine must be at least 5 characters long.")]
		[MaxLength(255, ErrorMessage = "Engine can't be longer than 255 characters.")]
		public string Engine { get; set; }

		[Required(ErrorMessage = "Publisher is required.")]
		public int PublisherId { get; set; }

		[Required(ErrorMessage = "Age label is required.")]
		public PegiAgeRating AgeLabel { get; set; }

		[Required(ErrorMessage = "Realease date is required.")]
		public DateTime ReleaseDate { get; set; }
		[Required(ErrorMessage = "User id is required.")]
		public int UserId { get; set; } = 4;
		[Required(ErrorMessage = "Game mode is required.")]
		public GameModes GameMode { get; set; }
		[Required(ErrorMessage = "Developer is required.")]
		public int DeveloperId { get; set; }
		[Required(ErrorMessage = "You must choose at least one genre.")]
		public List<int> Genres = new List<int>();
		[Required(ErrorMessage = "You must choose at least one platform.")]
		public List<int> Platforms = new List<int>();
		[Required(ErrorMessage = "You must choose cover image.")]
		public IFormFile Path { get; set; }
	}
}
