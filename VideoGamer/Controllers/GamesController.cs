using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO.Game;
using SharedModels.Fluent.Game;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
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
        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<Game>>>> Get([FromQuery] GameSearchRequest request)
        {
            var games = await _gamesService.All(request);
            return Ok(games);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try {
                var game = _gamesService.Find(id);
                return Ok(game);
            } catch(EntityNotFoundException e) {
                return NotFound(e.Message);
            } catch(Exception) {
                return StatusCode(500, "Server error please, try again.");
            }
        }

        // POST: api/Games
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateGameDTO dto)
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
                return StatusCode(500, "Server error, please try later.");
            }
        }

        // PUT: api/Games/5
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
