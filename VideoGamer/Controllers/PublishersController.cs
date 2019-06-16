using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;

namespace VideoGamer.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
		private readonly IPublisherService _publisherService;

		public PublishersController(IPublisherService publisherService)
		{
			_publisherService = publisherService;
		}

		// GET: api/Publishers
		[HttpGet]
		[Produces("application/json")]
		public async Task<ActionResult<PagedResponse<IEnumerable<Publisher>>>> Get([FromQuery] PublisherSearchRequest request)
        {
			var publishers = _publisherService.All(request);
			return Ok(publishers);
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
		[Produces("application/json")]
        public async Task<ActionResult<Publisher>> Get(int id)
        {
            try {
				var publisher = _publisherService.Find(id);
				return Ok(publisher);
			} catch (EntityNotFoundException e) {
				return NotFound(new { message = e.Message });
			} catch (Exception) {
				return StatusCode(500, new { Message = ServerErrorResponse.Message });
			}
        }

        // POST: api/Publishers
        [HttpPost]
		[Produces("application/json")]
		public void Post([FromBody] string value)
        {
        }

        // PUT: api/Publishers/5
        [HttpPut("{id}")]
		[Produces("application/json")]
		public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
		[Produces("application/json")]
		public void Delete(int id)
        {
        }
    }
}
