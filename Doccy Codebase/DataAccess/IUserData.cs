using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface IUserData
    {
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel?> GetUser(string email);
        Task InsertUser(UserModel user, string hashedPassword, string verification);
        Task VerifyUser(string email);
    }
}