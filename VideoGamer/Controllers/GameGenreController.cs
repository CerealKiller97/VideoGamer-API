using Aplication.Interfaces;
using Domain.Relations;
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
		public async Task<IActionResult> Create(GameGenre dto)
		{
			try {
				await _gameGenreService.AddGenreToGame(dto);
				return StatusCode(201);
			} catch (Exception e) {
				return StatusCode(500, e.Message);
			}
		}
    }
}