using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using EntityConfiguration;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;
using SharedModels.Fluent.User;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly VideoGamerDbContext _context;
		public ValuesController(VideoGamerDbContext context)
		{
			_context = context;
		}

		// GET api/values
		[HttpGet]
		public async Task<ActionResult<IEnumerable<string>>> Get()
		{
			var validator = new RegisterFluentValidator();
			var valid = await validator.ValidateAsync(new Register());

			if (!valid.IsValid)
			{
				return UnprocessableEntity(ValidationFormatter.Format(valid));
			}
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public async Task<IActionResult> Post(Register obj)
		{
			var validator = new RegisterFluentValidator();
			var valid = await validator.ValidateAsync(obj);

			if (!valid.IsValid)
			{
				return UnprocessableEntity(ValidationFormatter.Format(valid));
			}
			
			await _context.Users.AddAsync(new Domain.User
            {
				FirstName = obj.FirstName,
				LastName  = obj.LastName,
				Email     = obj.Email,
				Password  = obj.Password
			});
			await _context.SaveChangesAsync();
			return StatusCode(201);
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
