using Aplication.Pagination;
using SharedModels.DTO;

namespace Aplication.Interfaces
{
    public interface IService<ResponseDTO, InsertDTO, UpdateDTO, TSearch> where ResponseDTO : BaseDTO
    {
        PagedResponse<ResponseDTO> All(TSearch request);
        ResponseDTO Find(object id);
        void Create(InsertDTO dto);
        void Update(UpdateDTO dto);
        void Delete(object id);
        int Count();
    }
}
