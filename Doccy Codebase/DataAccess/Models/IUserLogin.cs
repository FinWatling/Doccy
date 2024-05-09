namespace DataAccessLibrary.Models
{
    public interface IUserLogin
    {
        string? email { get; set; }
        string? password { get; set; }
    }
}