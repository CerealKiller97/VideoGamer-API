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
using SharedModels.DTO.Developer;
using SharedModels.Fluent.Developer;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
	[Produces("application/json")]
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

		// GET: api/
		/// <summary>
		/// Search and filter all developers
		/// </summary>
		/// <returns>PagedResponse of Developers</returns>
		/// <response code="200"></response>
		[ProducesResponseType(200)]
		[HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<Developer>>>> Get([FromQuery]DeveloperSearchRequest request)
        {
            var developers = await _developerService.All(request);
            return Ok(developers);
        }

		// GET: api/Developers/5

		/// <summary>
		/// Find specific developer
		/// </summary>
		/// <returns>Wanted developer</returns>
		/// <response code="200"></response>
		/// <response code="404">Developer not found.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpGet("{id}")]
        public async Task<ActionResult<Developer>> Get(int id)
        {
            try {
                var developer = await _developerService.Find(id);
                return Ok(developer);
            } catch (EntityNotFoundException e) {
                return NotFound(new { e.Message });
            } catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
        }

		// POST: api/Developers
		/// <summary>
		/// Insert new developer
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="201">Successfully inserted.</response>
		/// <response code="422">Data is in invalid format.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(201)]
		[ProducesResponseType(422)]
		[ProducesResponseType(500)]
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
		/// <summary>
		/// Update specific developer
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="204">Successfully updated developer.</response>
		/// <response code="404">Developer not found.</response>
		/// <response code="422">Data is in invalid format.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(422)]
		[ProducesResponseType(500)]
		[HttpPut("{id}")]
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
		/// <summary>
		/// Delete specific developer
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="204">Successfully deleted developer.</response>
		/// <response code="404">Developer not found.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpDelete("{id}")]
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
