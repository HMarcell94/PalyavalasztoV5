using JWTAuth.API.Dtos;
using Palyavalaszto.Data.Entitites;
using System.Security.Cryptography;

namespace Palyavalaszto.Services
{
    public class UserService : IUserService
    {
        private readonly MyWorldDbContext _worldDbContext;
        private readonly IJwtService _jwtService;
        public UserService(MyWorldDbContext worldDbContext, IJwtService jwtService)
        {
            _worldDbContext = worldDbContext;
            _jwtService = jwtService;
        }

        private user FromUserRegistrationModelToUserMode(UserRegistrationDto userRegistration)
        {
            // Só generálása
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return new user
            {
                Email = userRegistration.Email,    
                Password = HashPassword(userRegistration.Password, salt), 
                salt = Convert.ToBase64String(salt)  
            };
        }


        private string HashPassword(string plainPassword, byte[] salt)
        {
            var rfcPassword = new Rfc2898DeriveBytes(plainPassword, salt, 1000, HashAlgorithmName.SHA1);
            byte[] rfcPasswordHash = rfcPassword.GetBytes(28);

            byte[] passwordHash = new byte[36];
            Array.Copy(salt, 0, passwordHash, 0, 16);
            Array.Copy(rfcPasswordHash, 0, passwordHash, 16, 20);

            return Convert.ToBase64String(passwordHash);
        }

        public async Task<(bool isUserRegistered, string Message)> RegisterNewUserAsync(UserRegistrationDto userRegistration)
        {
            var isUserExist = _worldDbContext.users.Any(_ => _.Email.ToLower() == userRegistration.Email.ToLower());
            if (isUserExist)
            {
                return (false, "Az Email cím már létezik");
            }
            var newUser = FromUserRegistrationModelToUserMode(userRegistration);
            _worldDbContext.users.Add(newUser);
            await _worldDbContext.SaveChangesAsync();
            return (true, "Siker");
        }

        public async Task<(bool isUserLoggedIn, string jwtToken)> LoginUserAsync(UserLoginDto userLogin)
        {
            var user = _worldDbContext.users.FirstOrDefault(u => u.Email.ToLower() == userLogin.Email.ToLower());
            if (user == null)
            {
                return (false, "A felhasználó nem található");
            }

            var salt = Convert.FromBase64String(user.salt);
            var hashedPassword = HashPassword(userLogin.Password, salt);
            if (user.Password != hashedPassword)
            {
                return (false, "Hibás jelszó");
            }

            var jwtToken = _jwtService.GenerateJwtToken(user); 
            return (true, jwtToken);
        }


    }
}
