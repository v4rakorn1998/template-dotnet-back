using Investment_back.Models.Request;
using Investment_back.Models.Response;

namespace Investment_back.Services.InternalService.Interface
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Login(AuthenticationRequest request);
        Task<RegisterResponse> Register(RegisterRequest request);
    }
}
