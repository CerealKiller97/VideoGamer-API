using System;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.DTO.Developer
{
	public class CreateDeveloperDTODataAnnotations
	{
		[Required(ErrorMessage = "Name is required.")]
		[MinLength(3, ErrorMessage = "Name must be at least 5 characters long.")]
		[MaxLength(200, ErrorMessage = "Name can't be longer than 200 characters long.")]
		public string Name { get; set; }
		[Required(ErrorMessage = "HQ is required.")]
		[MinLength(5, ErrorMessage = "HQ must be at least 5 characters long.")]
		[MaxLength(200, ErrorMessage = "HQ can't be longer than 200 characters long.")]
		public string HQ { get; set; }
		[Required]
		public DateTime? Founded { get; set; }
		[Required(ErrorMessage = "Website URL is required.")]
		[MinLength(10, ErrorMessage = "Website URL must be at least 10 characters long.")]
		[MaxLength(255, ErrorMessage = "Website URL can't be longer than 255 characters long.")]
		[Url(ErrorMessage = "Invalid URL address.")]
		public string Website { get; set; }
	}
}
