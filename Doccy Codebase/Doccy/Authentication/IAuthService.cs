using DataAccessLibrary.Models;

public interface IAuthService
{
    Task<bool> RegisterAsync(UserModel user, string password);
    Task<UserModel> VerifyLogin(string email, string password);
}