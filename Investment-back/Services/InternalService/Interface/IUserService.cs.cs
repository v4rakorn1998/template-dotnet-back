using Investment_back.Models.Response;

namespace Investment_back.Services.InternalService.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetUsers();
    }
}
