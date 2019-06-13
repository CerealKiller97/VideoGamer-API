using Aplication.Pagination;
using Aplication.Searches;
using SharedModels.DTO;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IService<ResponseDTO, InsertDTO, TSearch> where ResponseDTO : BaseDTO
    {
        Task<PagedResponse<ResponseDTO>> All(TSearch request);
        Task<ResponseDTO> Find(int id);
        Task Create(InsertDTO dto);
        Task Update(int id,InsertDTO dto);
        Task Delete(int id);
        Task<int> Count();
    }
}
