using System.Numerics;

namespace DataAccessLibrary.Models
{
    public class UserModel
    {
        public  int? UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? pass_hash { get; set; }
        public string? PhoneNumber { get; set; }
        public int EmailVerified { get; set; }
        public string? VerificationToken { get;}
    }
}
