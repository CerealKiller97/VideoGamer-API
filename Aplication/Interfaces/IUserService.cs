using Aplication.Searches;
using SharedModels.DTO;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
	public interface IUserService : IService<User, Register, UserSearchRequest>
	{
        Task<string> Login(Login dto);

        //Task Register(Register dto);
    }
}
