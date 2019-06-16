using Aplication.Exceptions;
using Aplication.Helpers.MyComicList.Application.Helpers;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using AutoMapper;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using PasswordHashing;
using SharedModels.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace EFServices.Services
{
	public class EFUserService : BaseService<Domain.User, UserSearchRequest>, IUserService
    {
		private readonly IRegisterService _registerService;
		private readonly IPasswordHasher _passwordHasher; 

		public EFUserService(VideoGamerDbContext context,
			IRegisterService registerService,
			IPasswordHasher passwordHasher
		) : base(context)
		{
			_registerService = registerService;
			_passwordHasher = passwordHasher;
		}

		public async Task<int> Count() => await _context.Users.CountAsync();

        public async Task Create(Register dto)
        {
			await _registerService.Register(dto);
        }

        public async Task Delete(int id)
        {
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

			if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResponse<User>> All(UserSearchRequest request)
        {

            var query = _context.Users.AsQueryable();

            return query.Select(user => new User
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            }).Paginate(request.PerPage, request.Page);

        }

        public async Task<User> Find(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Domain.User, User>());

            var userDTO = Mapper.Map<Domain.User, User>(user);

            return userDTO;
        }

        public async Task Update(int id, Register dto)
        {
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            if (user.FirstName != dto.FirstName)
			{
				user.FirstName = dto.FirstName;
			}

			if (user.LastName != dto.LastName)
			{
				user.LastName = dto.LastName;
			}

			if (user.Email != dto.Email)
			{
				user.Email = dto.Email;
			}

			bool arePasswordSame = _passwordHasher.ValidatePassword(dto.Password, user.Password);
			
			if (!arePasswordSame)
			{
				user.Password = _passwordHasher.HashPassword(dto.Password);
			}

			await _context.SaveChangesAsync();
        }

        
        protected override IQueryable<Domain.User> BuildQuery(IQueryable<Domain.User> query, UserSearchRequest request)
        {
            return null;
        }
    }
}

