using System;
using System.Threading.Tasks;
using Aplication.Interfaces;
using EntityConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;
using SharedModels.Fluent.User;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
	[AllowAnonymous]
	[Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
		private readonly IRegisterService _registerService;
		private readonly VideoGamerDbContext _context;

		public RegisterController(IRegisterService registerService, VideoGamerDbContext context)
		{
			_registerService = registerService;
			_context = context;
		}

		[HttpPost]
		[Route("")]
		public async Task<IActionResult> Register(Register dto)
		{
			var validator = new RegisterFluentValidator(_context);
			var errors = await validator.ValidateAsync(dto);
			if (!errors.IsValid)
			{
				return UnprocessableEntity(ValidationFormatter.Format(errors));
			}

			try
			{
				var user = await _registerService.Register(dto);
				//EMAIL
				return StatusCode(201);
			} catch (Exception) {

				throw;
			}
		}
	}
}