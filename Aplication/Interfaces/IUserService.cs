using SharedModels;

namespace Aplication.Interfaces
{
    public interface IUserService : IService<SharedModels.DTO.Register, SharedModels.Fluent.User.RegisterFluentValidator, SharedModels.Fluent.User.RegisterFluentValidator>
    {

    }
}
