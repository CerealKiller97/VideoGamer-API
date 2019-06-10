using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using AutoMapper;
using EntityConfiguration;
using SharedModels.DTO;
using SharedModels.Fluent.User;
using System.Linq;

namespace EFServices.Services
{
    public class EFUserService : BaseService<User, UserSearchRequest>, IUserService
    {
        public EFUserService(VideoGamerDbContext context) : base(context)
        {
        }

        public int Count() => _context.Users.Count();

        public void Create(Register dto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Register, Domain.User>());

            var DTO = Mapper.Map<Register, Domain.User>(dto);

            _context.Users.Add(DTO);

            _context.SaveChanges();
        }

        public void Delete(object id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public PagedResponse<User> All(UserSearchRequest request)
        {

            var query = _context.Users.AsQueryable();

            // GeneratePagedResponse(query, request);

            return null;

        }

        public User Find(object id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Domain.User, User>());

            var userDTO = Mapper.Map<Domain.User, User>(user);

            return userDTO;
        }

        public void Update(object id, Register dto)
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
    }
}

