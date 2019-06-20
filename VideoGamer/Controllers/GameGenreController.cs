using Aplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace VideoGamer.Controllers
{
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

		[HttpPost]
		[Route("")]
		public async Task<IActionResult> Create(int gameId, SharedModels.DTO.GameGenre.CreateGameGenreDTO dto)
		{
			try {
				await _gameGenreService.AddGenreToGame(gameId, dto);
				return StatusCode(201);
			} catch (Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpDelete]
		[Route("")]
		public async Task<IActionResult> Delete(int gameId, SharedModels.DTO.GameGenre.DeleteGameGenreDTO dto)
		{ 
			try {
				await _gameGenreService.RemoveGenreFrom(gameId, dto);
				return NoContent();
			} catch(Exception e) {
				return StatusCode(500);
			}
		}

    }
}