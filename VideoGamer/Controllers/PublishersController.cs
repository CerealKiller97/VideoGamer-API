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
using SharedModels.DTO.Publisher;
using SharedModels.Fluent.Publisher;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
	[Produces("application/json")]
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
		private readonly IPublisherService _publisherService;
		private readonly VideoGamerDbContext _context;

		public PublishersController(IPublisherService publisherService, VideoGamerDbContext context)
		{
			_publisherService = publisherService;
			_context = context;
		}

		// GET: api/Publishers
		/// <summary>
		/// Search and filter all publishers
		/// </summary>
		/// <returns>PagedResponse of Publisher</returns>
		/// <response code="200"></response>
		[ProducesResponseType(200)]
		[HttpGet]
		[Produces("application/json")]
		public async Task<ActionResult<PagedResponse<IEnumerable<Publisher>>>> Get([FromQuery] PublisherSearchRequest request)
        {
			var publishers = await _publisherService.All(request);
			return Ok(publishers);
        }

		// GET: api/Publishers/5
		/// <summary>
		/// Find specific publisher
		/// </summary>
		/// <returns>Wanted publisher</returns>
		/// <response code="200"></response>
		/// <response code="404">Publisher not found.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> Get(int id)
        {
            try {
				var publisher = await _publisherService.Find(id);
				return Ok(publisher);
			} catch (EntityNotFoundException e) {
				return NotFound(new { message = e.Message });
			} catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
        }

		// POST: api/Publishers
		/// <summary>
		/// Insert new publisher
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="201">Successfully inserted.</response>
		/// <response code="422">Data is in invalid format.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(201)]
		[ProducesResponseType(422)]
		[ProducesResponseType(500)]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreatePublisherDTO dto)
        {
			var validator = new PublisherFluentValidator(_context);
			var errors = await validator.ValidateAsync(dto);
			if (!errors.IsValid)
			{
				return UnprocessableEntity(ValidationFormatter.Format(errors));
			}

			try {
				await _publisherService.Create(dto);
				return StatusCode(201);
			} catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
		}

		// PUT: api/Publishers/5
		/// <summary>
		/// Update specific publisher
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="204">Successfully updated publisher.</response>
		/// <response code="404">Publisher not found.</response>
		/// <response code="422">Data is in invalid format.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(422)]
		[ProducesResponseType(500)]
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] CreatePublisherDTO dto)
        {
			var validator = new PublisherUpdateFluentValidator(_context, id);
			var errors = await validator.ValidateAsync(dto);
			if (!errors.IsValid)
			{
				return UnprocessableEntity(ValidationFormatter.Format(errors));
			}

			try {
				await _publisherService.Update(id, dto);
				return NoContent();
			} catch (EntityNotFoundException e) {
				return NotFound(new { e.Message });
			} catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
		}

		// DELETE: api/ApiWithActions/5
		/// <summary>
		/// Delete specific publisher
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="204">Successfully deleted publisher.</response>
		/// <response code="404">Publisher not found.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
        {
			try {
				await _publisherService.Delete(id);
				return NoContent();
			} catch (EntityNotFoundException e) {
				return NotFound(new { e.Message });
			} catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
		}
    }
}
