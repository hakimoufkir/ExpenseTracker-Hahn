using webAPI.DTOs;
using webAPI.Models;

namespace webAPI.Services.interfaces
{
    public interface IUserService
    {
        Task<Response> RegisterUserAsync(SignUpRequest signUpRequest);
        Task<Response> AuthenticateUserAsync(SignInRequest signInRequest, HttpContext httpContext);
        Task<List<UserClaim>> GetUserClaimsAsync(HttpContext httpContext);
        Task<Response> SignOutUserAsync(HttpContext httpContext);

    }
}
