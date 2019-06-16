using System;
using System.Threading.Tasks;
using Aplication.Helpers;
using Aplication.Interfaces;
using EntityConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;
using SharedModels.Fluent.User;
using SharedModels.Formatters;
using VideoGamer.Mailer;

namespace VideoGamer.Controllers
{
	[AllowAnonymous]
	[Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
		private readonly IRegisterService _registerService;
		private readonly VideoGamerDbContext _context;
		private readonly IEmailService _emailService;

		public RegisterController(IRegisterService registerService, VideoGamerDbContext context, IEmailService emailService) 
		{
			_registerService = registerService;
			_context = context;
			_emailService = emailService;
		}
		/// <summary>
		/// Registe new user
		/// </summary>
		/// <returns>Status code</returns>
		/// <response code="201">Successfully registered.</response>
		/// <response code="422">Data is in invalid format.</response>
		/// <response code="500">Server error, please try later.</response>
		[ProducesResponseType(201)]
		[ProducesResponseType(422)]
		[ProducesResponseType(500)]
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

			try {
				var user = await _registerService.Register(dto);
				_emailService.Body = "You have succcessfully registered.";
				_emailService.Subject = "Registration mail";
				_emailService.ToEmail = user.Email;
				_emailService.Send();
				//EMAIL
				return StatusCode(201);
			} catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message });
			}
		}
	}
}