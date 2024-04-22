using Palyavalaszto.Data.Entitites;

namespace Palyavalaszto.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(user users);
    }
}
