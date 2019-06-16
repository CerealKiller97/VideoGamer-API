using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;
using SharedModels.Fluent.User;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
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
        [HttpGet("{id}", Name = "Get")]
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

     
        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Register dto)
        {
			var validator = new RegisterFluentValidator(_context);
			var errors = await validator.ValidateAsync(dto);
			if (!errors.IsValid)
			{
				return UnprocessableEntity(ValidationFormatter.Format(errors));
			}

			try
            {
                await userService.Create(dto);
                return StatusCode(201);
            }
            catch (FluentValidationCustomException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error please try again.");
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

		// LOGIN

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login(Login dto)
		{
			var x = 1;
			return Ok("string");
			//try
			//{
			//	string token = await userService.Login(login);
			//	return Ok(new { token });
			//} catch (EntityNotFoundException e)
			//{
			//	return BadRequest(new { message = e.Message });
			//} catch (PasswordNotValidException e)
			//{
			//	return BadRequest(new { message = e.Message });
			//} catch (Exception)
			//{
			//	return StatusCode(500);
			//}

		}
	}
}
