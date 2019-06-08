using Aplication.Searches;
using SharedModels.DTO;

namespace Aplication.Interfaces
{
    public interface IDeveloperService : IService<Developer, CreateDeveloperDTO, DeveloperSearchRequest>
    {
        
    }
}