using System;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;

namespace VideoGamer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

		[HttpPost]
		public async Task<IActionResult> Login(Login dto)
		{
			try
			{
				string token = await _userService.Login(dto);
				HttpContext
					.Response
					.Cookies
					.Append("token", token, new CookieOptions { HttpOnly = true });

				return Ok(new { message = "You have succesfully logged in." });
			} catch (EntityNotFoundException e)
			{
				return BadRequest(new { message = e.Message });
			} catch (PasswordNotValidException e)
			{
				return BadRequest(new { message = e.Message });
			} catch (Exception)
			{
				return StatusCode(500);
			}

		}
	}
}