using System;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Helpers;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;
using SharedModels.Fluent.User;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
		private readonly VideoGamerDbContext _context;

		public UsersController(IUserService service, VideoGamerDbContext context)
		{
			_userService = service;
			_context = context;
		}

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<PagedResponse<User>>> Get([FromQuery] UserSearchRequest request)
        {
            var users = await _userService.All(request);
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<User>> Get(int id)
        {
            try {
                var user = await _userService.Find(id);
                return Ok(user);
            } catch(EntityNotFoundException e) {
                return NotFound(new { e.Message });
            } catch(Exception) {
                return StatusCode(500, new { ServerErrorResponse.Message });
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
		[Produces("application/json")]
		public async Task<IActionResult> Put(int id, [FromBody] Register dto)
        {
			var validator = new UserUpdateFluentValidator(_context, id);
			var errors = await validator.ValidateAsync(dto);
			if (!errors.IsValid)
			{
				return UnprocessableEntity(ValidationFormatter.Format(errors));
			}

			try {
				await _userService.Update(id, dto);
                return NoContent();
            } catch (EntityNotFoundException e) {
                return NotFound(new { e.Message });
            } catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
		[Produces("application/json")]
		public async Task<IActionResult> Delete(int id)
        {
            try {
				await _userService.Delete(id);
                return NoContent();
            } catch (EntityNotFoundException e) {
                return NotFound(new { e.Message });
            } catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
        }
	}
}
