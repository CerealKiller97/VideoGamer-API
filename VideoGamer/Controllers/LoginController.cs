using System;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Helpers;
using Aplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;
using SharedModels.Fluent.User;
using SharedModels.Formatters;

namespace VideoGamer.Controllers
{
	[AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService service)
        {
			_loginService = service;
        }

		[HttpPost]
		[Route("")]
		public async Task<IActionResult> Login(Login dto)
		{

			var validator = new LoginFluentValidator();
			var errors = await validator.ValidateAsync(dto);

			if (!errors.IsValid)
			{
				return UnprocessableEntity(ValidationFormatter.Format(errors));
			}

			try {
				string token = await _loginService.Login(dto);
				HttpContext
					.Response
					.Cookies
					.Append("token", token, new CookieOptions { HttpOnly = true });

				return Ok(new { message = "You have succesfully logged in.", token });
			} catch (EntityNotFoundException e) {
				return BadRequest(new { message = e.Message });
			} catch (PasswordNotValidException e) {
				return BadRequest(new { message = e.Message });
			} catch (Exception) {
				return StatusCode(500, new { ServerErrorResponse.Message }); ;
			}
		}
	}
}