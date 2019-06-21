using Aplication.Searches;
using SharedModels.DTO;
using SharedModels.DTO.Publisher;

namespace Aplication.Interfaces
{
    public interface IPublisherService : IService<Publisher, CreatePublisherDTO, PublisherSearchRequest>
    {
        
    }
}