using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Interfaces;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PasswordHashing;
using SharedModels.DTO;

namespace EFServices.Services
{
	public class EFLoginService : ILoginService
	{
		private readonly VideoGamerDbContext _context;
		private readonly IConfiguration _config;
		private readonly IPasswordHasher _hasher;

		public EFLoginService(VideoGamerDbContext context, IConfiguration config, IPasswordHasher hasher)
		{
			_context = context;
			_config = config;
			_hasher = hasher;
		}

		public async Task<string> Login(Login dto)
		{
			var user = await _context.Users
						.Where(u => u.Email == dto.Email)
				 // TODO: Add activated at
						.FirstOrDefaultAsync();

			if (user == null)
			{
				throw new EntityNotFoundException("User");
			}

			if (!_hasher.ValidatePassword(dto.Password, user.Password))
			{
				throw new PasswordNotValidException();
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			string key = _config.GetSection("JwtKey").Value;
			var keyBytes = Encoding.ASCII.GetBytes(key);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim("id", user.Id.ToString()),
					new Claim(ClaimTypes.Email, user.Email),
					new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
