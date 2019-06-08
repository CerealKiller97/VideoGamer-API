using System;
using System.Collections.Generic;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Searches;
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
        public UsersController(IUserService userService) => this.userService = userService;

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get(UserSearchRequest request)
        {
            return Ok(userService.All(request));
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
        public IActionResult Post([FromBody] Register dto)
        {
            try
            {
                userService.Create(dto);
                return StatusCode(201);
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
                userService.Update(dto);
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
