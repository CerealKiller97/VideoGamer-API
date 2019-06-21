using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Helpers;
using Aplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO.GamePlatform;

namespace VideoGamer.Controllers
{
	[Authorize]
	[Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GamePlatformController : ControllerBase
    {
		private readonly IGamePlatformService _gamePlatformService;
		public GamePlatformController(IGamePlatformService service)
		{
			_gamePlatformService = service;
		}

		[HttpPost("{id}")]
		public async Task<IActionResult> AddPlatform(int id, GamePlatform dto)
		{
			try {
				await _gamePlatformService.Add(id, dto);
				return StatusCode(201);
			} catch (EntityNotFoundException e ) {
				return NotFound(new { e.Message });
			} catch (DataAlreadyExistsException e) {
				return Conflict(new { Message = "Game is already on that platform." });
				} catch (Exception) {
				return StatusCode(500, ServerErrorResponse.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePlatform(int id, GamePlatform dto)
		{
			try
			{
				await _gamePlatformService.Delete(id, dto);
				return NoContent();
			} catch (EntityNotFoundException e) {
				return NotFound(new { e.Message });
			} catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
		}
	}
}