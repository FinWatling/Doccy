namespace Doccy
{
    public interface IUserContext
    {
        bool IsLoggedIn { get; set; }
        string Username { get; set; }
    }
}