using Aplication.Pagination;
using Domain;

namespace Aplication.Interfaces
{
    public interface IService<ResponseDTO, InsertDTO, UpdateDTO, TSearch> where ResponseDTO : AbstractModel
    {
        PagedResponse<ResponseDTO> All(TSearch request);
        ResponseDTO Find(object id);
        void Create(InsertDTO dto);
        void Update(UpdateDTO dto);
        void Delete(object id);
        int Count();
    }
}
