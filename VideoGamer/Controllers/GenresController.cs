using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Helpers;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO.Genre;
using SharedModels.Fluent.Genre;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
	[Produces("application/json")]
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
		private readonly IGenreService _genreService;
		private readonly VideoGamerDbContext _context;

		public GenresController(IGenreService genreService, VideoGamerDbContext context)
		{
			_genreService = genreService;
			_context = context;
		}

		// GET: api/Genres
		/// <summary>
		/// Search and filter all genres
		/// </summary>
		/// <returns>PagedResponse of Genre</returns>
		/// <response code="200"></response>
		[ProducesResponseType(200)]
		[HttpGet]
		public async Task<ActionResult<PagedResponse<IEnumerable<Genre>>>> Get([FromQuery] GenreSearchRequest request)
        {
			var genres = await _genreService.All(request);
			return Ok(genres);
        }

		// GET: api/Genres/5
		/// <summary>
		/// Find specific genre
		/// </summary>
		/// <returns>Wanted genre</returns>
		/// <response code="200"></response>
		/// <response code="404">Genre not found.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            try {
				var genre = await _genreService.Find(id);
				return Ok(genre);
			} catch (EntityNotFoundException e) {
				return NotFound(e.Message);
			} catch (Exception ) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
        }

		// POST: api/Genres
		/// <summary>
		/// Insert new genre
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="201">Successfully inserted.</response>
		/// <response code="422">Data is in invalid format.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(201)]
		[ProducesResponseType(422)]
		[ProducesResponseType(500)]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateGenreDTO dto)
        {
			var validator = new GenreFluentValidator(_context);
			var errors = await validator.ValidateAsync(dto);

			if (!errors.IsValid)
			{
				return UnprocessableEntity(ValidationFormatter.Format(errors));
			}

			try {
				await _genreService.Create(dto);
				return StatusCode(201);
			} catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			} 
        }

		// PUT: api/Genres/5
		/// <summary>
		/// Update specific genre
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="204">Successfully updated genre.</response>
		/// <response code="404">Genre not found.</response>
		/// <response code="422">Data is in invalid format.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(422)]
		[ProducesResponseType(500)]
		[HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CreateGenreDTO dto)
        {
			var validator = new GenreUpdateFluentValidator(_context, id);
			var errors = await validator.ValidateAsync(dto);

			if (!errors.IsValid)
			{
				return UnprocessableEntity(ValidationFormatter.Format(errors));
			}

			try {
				await _genreService.Update(id, dto);
				return NoContent();
			} catch (EntityNotFoundException e) {
				return NotFound(new { e.Message });
			} catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
		}

		// DELETE: api/ApiWithActions/5
		/// <summary>
		/// Delete specific genre
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="204">Successfully deleted genre.</response>
		/// <response code="404">Genre not found.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
        {
			try {
				await _genreService.Delete(id);
				return NoContent();
			} catch (EntityNotFoundException e) {
				return NotFound(e.Message);
			} catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
		}
    }
}
