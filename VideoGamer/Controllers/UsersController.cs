using System;
using System.Collections.Generic;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;

namespace VideoGamer.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
		private readonly VideoGamerDbContext _context;

		public UsersController(IUserService service, VideoGamerDbContext context)
		{
			 userService = service;
			_context = context;
		}

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get([FromQuery] UserSearchRequest request)
        {
            var users = userService.All(request);
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public ActionResult<User> Get(int id)
        {
            try {
                var user = userService.Find(id);
                return Ok(user);
            } catch(EntityNotFoundException e) {
                return NotFound(e.Message);
            } catch(Exception) {
                return StatusCode(500, "Server error please try later.");
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Register dto)
        {
            try
            {
                userService.Update(id, dto);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error please try again.");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                userService.Delete(id);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error please try again.");
            }
        }

		}
}
