using SharedModels.DTO;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
	public interface ILoginService
	{
		Task<string> Login(Login dto);
	}
}
