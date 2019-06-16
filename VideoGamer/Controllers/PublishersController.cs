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
using SharedModels.Fluent.Publisher;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
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
		[HttpGet]
		[Produces("application/json")]
		public async Task<ActionResult<PagedResponse<IEnumerable<Publisher>>>> Get([FromQuery] PublisherSearchRequest request)
        {
			var publishers = await _publisherService.All(request);
			return Ok(publishers);
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
		[Produces("application/json")]
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
        [HttpPost]
		[Produces("application/json")]
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
        [HttpPut("{id}")]
		[Produces("application/json")]
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
				await _publisherService.Delete(id);
				return NoContent();
			} catch (EntityNotFoundException e) {
				return NotFound(e.Message);
			} catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
		}
    }
}
