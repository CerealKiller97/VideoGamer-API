using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VideoGamer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gamesService;

        public GamesController(IGameService gamesService) => _gamesService = gamesService;


        // GET: api/Games
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var game = _gamesService.Find(id);
                return Ok(game);
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            } 
            catch(Exception)
            {
                return StatusCode(500, "Server error please, try again.");
            }
        }

        // POST: api/Games
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
