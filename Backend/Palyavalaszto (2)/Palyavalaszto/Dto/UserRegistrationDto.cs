namespace JWTAuth.API.Dtos
{
public class UserRegistrationDto
{
public int UserID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }


}

    public class UserLoginDto
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }


    }


}