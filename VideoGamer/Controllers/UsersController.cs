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
	[Produces("application/json")]
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
		/// <summary>
		/// Search and filter all users
		/// </summary>
		/// <returns>PagedResponse of Users</returns>
		/// <response code="200"></response>
		[ProducesResponseType(200)]
		[HttpGet]
        public async Task<ActionResult<PagedResponse<User>>> Get([FromQuery] UserSearchRequest request)
        {
            var users = await _userService.All(request);
            return Ok(users);
        }

		// GET: api/Users/5
		/// <summary>
		/// Find specific user
		/// </summary>
		/// <returns>Wanted user</returns>
		/// <response code="200"></response>
		/// <response code="404">User not found.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpGet("{id}")]
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
		/// <summary>
		/// Update specific user
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="204">Successfully updated user.</response>
		/// <response code="404">User not found.</response>
		/// <response code="422">Data is in invalid format.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(422)]
		[ProducesResponseType(500)]
		[HttpPut("{id}")]
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
		/// <summary>
		/// Delete specific user
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="204">Successfully deleted user.</response>
		/// <response code="404">User not found.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpDelete("{id}")]
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
