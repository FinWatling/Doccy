namespace Doccy
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            //For account creation
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            //For login verification
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}