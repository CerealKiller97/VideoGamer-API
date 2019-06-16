using Aplication.Exceptions;
using Aplication.Helpers.MyComicList.Application.Helpers;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using AutoMapper;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PasswordHashing;
using SharedModels.DTO;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EFServices.Services
{
	public class EFUserService : BaseService<Domain.User, UserSearchRequest>, IUserService
    {
        private IPasswordHasher _hasher;
        private IConfiguration _config;

        public EFUserService(VideoGamerDbContext context, IPasswordHasher hasher, IConfiguration configuration) : base(context)
        {
            _hasher = hasher;
            _config = configuration;
        }

        public async Task<int> Count() => await _context.Users.CountAsync();

        public async Task Create(Register dto)
        {
            Domain.User user = new Domain.User()
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

            // TODO: Send Verification Email
        }

        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

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
            var user = await _context.Users.FindAsync(id);

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
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            // validation passed?

            Mapper.Initialize(cfg => cfg.CreateMap<Register, Domain.User>());

            user = Mapper.Map<Register, Domain.User>(dto); // ??

            _context.Users.Update(user);
            _context.SaveChanges();
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

            if(!_hasher.ValidatePassword(dto.Password, user.Password))
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
        protected override IQueryable<Domain.User> BuildQuery(IQueryable<Domain.User> query, UserSearchRequest request)
        {
            return null;
        }
    }
}

