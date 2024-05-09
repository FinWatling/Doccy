using DataAccessLibrary.Models;
using DataAccessLibrary;
using Doccy;
using Doccy.Communication;

public class AuthService(IUserData userData) : IAuthService
{
    private readonly IUserData _userData = userData;

    public async Task<bool> RegisterAsync(UserModel user, string password)
    {
        EmailService emailService = new EmailService();
        try
        {
            Random generator = new();

            // Ensure Email is lowercase
            user.Email = user.Email.ToLower();

            // Check to see if a user already exists with this email
            UserModel? check = await _userData.GetUser(user.Email);

            if (check != null)
            {
                return false; // User already exists
            }
            // Hash the password
            string hashedPassword = PasswordHasher.HashPassword(password);


            // To create verification code
            string verification = generator.Next(0, 1000000).ToString("D6");



            // Insert user into database with hashed password
            await _userData.InsertUser(user, hashedPassword, verification);

            // Send Verification Email
            emailService.SendVerificationEmail(user.Email, verification);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<UserModel> VerifyLogin(string email, string password)
    {
        try
        {
            // Get user info from database based on username (email) SSF USER WHERE EMAIL = USERNAME

            UserModel user = await _userData.GetUser(email);

            if (user == null)
            {
                return null;
            }

            // Verify the password
            if (!PasswordHasher.VerifyPassword(password, user.pass_hash))
            {
                return null; // Password verification failed
            }

            return user;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> RetryVerification(string username)
    {
        UserModel? user = await _userData.GetUser(username);

        if (user.VerificationToken != null)
        {
            EmailService emailService = new EmailService();

            emailService.SendVerificationEmail(username, user.VerificationToken);

            return true;
        }

        return false;

    }

    public async Task<bool> VerifyEmail(string username, string code)
    {
        try
        {
            // Get user info from database based on username (email) SSF USER WHERE EMAIL = USERNAME

            UserModel? user = await _userData.GetUser(username);

            if (user.VerificationToken != null && user.VerificationToken == code && user != null)
            {
                await _userData.VerifyUser(username);

                return true;

            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}
 