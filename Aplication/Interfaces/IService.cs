using Aplication.Pagination;
using Aplication.Searches;
using SharedModels.DTO;

namespace Aplication.Interfaces
{
    public interface IService<ResponseDTO, InsertDTO, TSearch> where ResponseDTO : BaseDTO
    {
        PagedResponse<ResponseDTO> All(TSearch request);
        ResponseDTO Find(object id);
        void Create(InsertDTO dto);
        void Update(InsertDTO dto);
        void Delete(object id);
        int Count();
    }
}
