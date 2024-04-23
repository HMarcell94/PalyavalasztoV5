namespace PalyavalsztoV4
{
    public class TokenStore
    {
        public static string Token { get; private set; }

        public static void Store(string token)
        {
            Token = token;
        }
    }
}
