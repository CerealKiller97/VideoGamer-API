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
            _context.Users.Add(new Domain.User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password
            });

            _context.SaveChanges();
        }

        public void Update(Register dto)
        {
            throw new System.NotImplementedException();
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
            return new PagedResponse<User>();
        }

        public PagedResponse<User> All(BaseSearchRequest request)
        {
            throw new System.NotImplementedException();
        }

        public User Find(object id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            //Mapper.Initialize(cfg => cfg.CreateMap<user, User>());

            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
        }

        public void Update(Register dto, object id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new EntityNotFoundException("User"); 
            }

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Password = dto.Password;
            
            _context.Users.Update(user);
            _context.SaveChanges();
        }


        protected override IQueryable<User> BuildingQuery(IQueryable<User> query, UserSearchRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}

