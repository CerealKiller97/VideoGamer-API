using System.Threading.Tasks;
using Aplication.Interfaces;
using EntityConfiguration;
using PasswordHashing;
using SharedModels.DTO;

namespace EFServices.Services
{
	public class EFRegisterService : IRegisterService
	{
		private readonly VideoGamerDbContext _context;
		private readonly IPasswordHasher _hasher;

		public EFRegisterService(VideoGamerDbContext context, IPasswordHasher hasher)
		{
			_context = context;
			_hasher = hasher;
		}

		public async Task<Domain.User> Register(Register dto)
		{
			Domain.User user = new Domain.User
			{
				Email = dto.Email,
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Password = _hasher.HashPassword(dto.Password),
				ActivatedAt = null,
				LastLogin = null
			};

			await _context.Users.AddAsync(user);

			await _context.SaveChangesAsync();

			return user;
		}
	}
}
