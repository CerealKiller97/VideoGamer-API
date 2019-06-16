using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO.Genre;

namespace VideoGamer.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
		private readonly IGenreService genreService;

		public GenresController(IGenreService service)
		{
			genreService = service;
		}
        // GET: api/Genres
        [HttpGet]
		public async Task<ActionResult<PagedResponse<IEnumerable<Genre>>>> Get([FromQuery] GenreSearchRequest request)
        {
			var genres = await genreService.All(request);
			return Ok(genres);
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
		[Produces("application/json")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            try {
				var genre = await genreService.Find(id);
				return Ok(genre);
			} catch (EntityNotFoundException e) {
				return NotFound(e.Message);
			} catch (Exception ) {
				return StatusCode(500,new { message = "Server error plase try later." });
			}
        }

        // POST: api/Genres
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateGenreDTO dto)
        {
			try {
				await genreService.Create(dto);
				return StatusCode(201);
			} catch (Exception) {
				return StatusCode(500, new { message = "Server error please try later." });
			} 
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
