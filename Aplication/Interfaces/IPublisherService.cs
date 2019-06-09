using Aplication.Searches;
using SharedModels.DTO;

namespace Aplication.Interfaces
{
    public interface IPublisherService : IService<Publisher,CreateDeveloperDTO, PublisherSearchRequest>
    {
        
    }
}