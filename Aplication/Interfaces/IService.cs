using System.Collections.Generic;

namespace Aplication.Interfaces
{
    public interface IService<ResponseDTO, InsertDTO, UpdateDTO>
    {
        IEnumerable<ResponseDTO> All();
        ResponseDTO Find(object id);
        void Create(InsertDTO dto);
        void Update(UpdateDTO dto);
        void Delete(object id);
        int Count();
    }
}
