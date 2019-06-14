using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using AutoMapper;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace EFServices.Services
{
    public class EFUserService : BaseService<Domain.User, UserSearchRequest>, IUserService
    {
        public EFUserService(VideoGamerDbContext context) : base(context)
        {
        }

        public async Task<int> Count() => await _context.Users.CountAsync();

        public async Task Create(Register dto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Register, Domain.User>());

            var DTO = Mapper.Map<Register, Domain.User>(dto);

            _context.Users.Add(DTO);

            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task<PagedResponse<SharedModels.DTO.User>> All(UserSearchRequest request)
        {

            var query = _context.Users.AsQueryable();

            // GeneratePagedResponse(query, request);

            return null;

        }

        public async Task<SharedModels.DTO.User> Find(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Domain.User, SharedModels.DTO.User>());

            var userDTO = Mapper.Map<Domain.User, SharedModels.DTO.User>(user);

            return userDTO;
        }

        public async Task Update(int id, Register dto)
        {
            var user = _context.Users.Find(id);

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

        public async Task<SharedModels.DTO.User> Login(Login dto)
        {
            var user = await _context.Users
                 .Where(u => u.Email == dto.Email)
                 .Where(u => u.Password == dto.Password)
                 .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Domain.User, SharedModels.DTO.User>());

            var userDTO = Mapper.Map<Domain.User, SharedModels.DTO.User>(user);

            return userDTO;
        }

        protected override IQueryable<Domain.User> BuildQuery(IQueryable<Domain.User> query, UserSearchRequest request)
        {
            return null;
        }
    }
}

