using Aplication.Searches;
using SharedModels.DTO.Developer;

namespace Aplication.Interfaces
{
    public interface IDeveloperService : IService<Developer, CreateDeveloperDTO, DeveloperSearchRequest>
    {
        
    }
}