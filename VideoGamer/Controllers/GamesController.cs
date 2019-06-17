using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO.Game;
using SharedModels.Fluent.Game;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
	[Produces("application/json")]
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gamesService;
        private readonly VideoGamerDbContext _context;

        public GamesController(IGameService gamesService, VideoGamerDbContext context)
        {
            _gamesService = gamesService;
            _context = context;
        }


		// GET: api/Games
		/// <summary>
		/// Search and filter all games
		/// </summary>
		/// <returns>PagedResponse of Games</returns>
		/// <response code="200"></response>
		[ProducesResponseType(200)]
		[HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<Game>>>> Get([FromQuery] GameSearchRequest request)
        {
			var games = await _gamesService.All(request);
			return Ok(games);
        }

		// GET: api/Games/5
		/// <summary>
		/// Find specific game
		/// </summary>
		/// <returns>Wanted game</returns>
		/// <response code="200"></response>
		/// <response code="404">Game not found.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpGet("{id}")]
        public async Task<ActionResult<Game>> Get(int id)
        {
            try {
                var game = _gamesService.Find(id);
                return Ok(game);
            } catch(EntityNotFoundException e) {
                return NotFound(new { e.Message });
            } catch(Exception) {
                return StatusCode(500, "Server error please, try again.");
            }
        }

		// POST: api/Games
		/// <summary>
		/// Insert new game
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="201">Successfully inserted.</response>
		/// <response code="422">Data is in invalid format.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(201)]
		[ProducesResponseType(422)]
		[ProducesResponseType(500)]
		[HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateGameDTO dto)
        {
            var validator = new GameFluentValidator(_context);
            var errors = await validator.ValidateAsync(dto);

            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }

            try {
                await _gamesService.Create(dto);
                return StatusCode(201);
            } catch (Exception e) {
				var s = 2;
                return StatusCode(500, e);
            }
        }

		// PUT: api/Games/5
		/// <summary>
		/// Update specific game
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="204">Successfully updated game.</response>
		/// <response code="404">Game not found.</response>
		/// <response code="422">Data is in invalid format.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(422)]
		[ProducesResponseType(500)]
		[HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] CreateGameDTO dto)
        {
			var validator = new GameFluentValidator(_context);
			var errors = await validator.ValidateAsync(dto);

			if (!errors.IsValid)
			{
				return UnprocessableEntity(ValidationFormatter.Format(errors));
			}

			try {
				await _gamesService.Update(id, dto);
				return NoContent();
			} catch (EntityNotFoundException e) {
				return NotFound(new { message = e.Message });
			} catch (Exception) {
				return StatusCode(500, new { message = "Server error, please try later." });
			}
		}

		// DELETE: api/ApiWithActions/5
		/// <summary>
		/// Delete specific game
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="204">Successfully deleted game.</response>
		/// <response code="404">Game not found.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
			try
			{
				await _gamesService.Delete(id);
				return NoContent();
			} catch (EntityNotFoundException e) {
				return NotFound(new { message = e.Message });
			} catch (Exception) {
				return StatusCode(500, new { message = "Server error, please try later." });
			}
		}
    }
}
