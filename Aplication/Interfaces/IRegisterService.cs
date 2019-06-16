using Domain;
using SharedModels.DTO;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
	public interface IRegisterService
	{
		Task<Domain.User> Register(Register dto);
	}
}
