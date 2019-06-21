using Aplication.Exceptions;
using Aplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace VideoGamer.Controllers
{
	[Produces("application/json")]
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameGenreController : ControllerBase
    {
		private readonly IGameGenreService _gameGenreService;

		public GameGenreController(IGameGenreService gameGenreService)
		{
			_gameGenreService = gameGenreService;
		}
		/// <summary>
		/// Add genre to specific game
		/// </summary>
		/// <returns>Inserted</returns>
		/// <response code="201"></response>
		/// <response code="404">Game not found.</response>
		/// <response code="500">Server error, please try later.</response>
		[HttpPost("{id}")]
		[ProducesResponseType(201)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> Create(int id, SharedModels.DTO.GameGenre.CreateGameGenreDTO dto)
		{
			try {
				await _gameGenreService.AddGenreToGame(id, dto);
				return StatusCode(201);
			} catch (EntityNotFoundException e) {
				return NotFound(new { e.Message });
			} catch(DataAlreadyExistsException e) {
				return Conflict(new { e.Message });
			} catch (Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		/// <summary>
		/// Delete specific genre from a game
		/// </summary>
		/// <returns>Wanted game</returns>
		/// <response code="200"></response>
		/// <response code="404">Game not found.</response>
		/// <response code="500">Server error, please try later.</response>
		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> Delete(int id, [FromBody] SharedModels.DTO.GameGenre.DeleteGameGenreDTO dto)
		{
			try {
				await _gameGenreService.RemoveGenreFrom(id, dto);
				return NoContent();
			} catch (EntityNotFoundException e) {
				return NotFound(new { e.Message });
			} catch(Exception e) {
				return StatusCode(500);
			}
		}
    }
}