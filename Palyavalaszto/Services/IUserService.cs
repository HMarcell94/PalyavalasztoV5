using JWTAuth.API.Dtos;

namespace Palyavalaszto.Services
{
    public interface IUserService
    {
        Task<(bool isUserRegistered, string Message)> RegisterNewUserAsync(UserRegistrationDto userRegistration);

        Task<(bool isUserLoggedIn, string jwtToken)> LoginUserAsync(UserLoginDto userLogin);
    }
}