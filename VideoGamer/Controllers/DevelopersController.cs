using System;
using System.Collections.Generic;
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
using SharedModels.Fluent.Developer;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly IDeveloperService _developerService;
        private readonly VideoGamerDbContext _context;

        public DevelopersController(IDeveloperService developerService, VideoGamerDbContext context)
        {
            _developerService = developerService;
            _context = context;
        }

        // GET: api/Developers
        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<Developer>>>> Get([FromQuery]DeveloperSearchRequest request)
        {
            var developers = await _developerService.All(request);
            return Ok(developers);
        }

        // GET: api/Developers/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(int id)
        {
            try {
                var developer = await _developerService.Find(id);
                return Ok(developer);
            } catch (EntityNotFoundException e) {
                return NotFound(e.Message);
            } catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
        }

        // POST: api/Developers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDeveloperDTO dto)
        {
            var validator = new DeveloperFluentValidatior(_context);
            var errors = await validator.ValidateAsync(dto);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }

            try {
                await _developerService.Create(dto);
                return StatusCode(201);
            } catch(Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
        }

        // PUT: api/Developers/5
        [HttpPut("{id}")]
		[Produces("application/json")]
        public async Task<IActionResult> Put(int id, [FromBody] CreateDeveloperDTO dto)
        {
			var validator = new DevelopUpdateFluentValidator(_context, id);
			var errors = await validator.ValidateAsync(dto);
			if (!errors.IsValid)
			{
				return UnprocessableEntity(ValidationFormatter.Format(errors));
			}

			try {
                await _developerService.Update(id, dto);
                return NoContent();
            } catch (EntityNotFoundException e) {
                return NotFound(e.Message);
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
                await _developerService.Delete(id);
                return NoContent();
            } catch (EntityNotFoundException e) {
                return NotFound(e.Message);
            } catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
        }
    }
}
