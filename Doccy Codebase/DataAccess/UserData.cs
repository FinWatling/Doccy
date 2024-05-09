using DataAccessLibrary.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DataAccessLibrary
{
    public class UserData(IDataAccess db) : IUserData
    {
        private readonly IDataAccess _db = db;

        public Task<List<UserModel>> GetAllUsers()
        {
            string sql = "SELECT * FROM [dbo].[User]";

            return _db.LoadData<UserModel, dynamic>(sql, new { });

        }

        public Task InsertUser(UserModel user, string hashedPassword, string verification)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("User data or hashed password cannot be null or empty.");

            if (!IsValidEmail(user.Email))
                throw new ArgumentException("Invalid email format.");

            // Use parameterized query to insert user data
            string sql = @"
        INSERT INTO [dbo].[User] (firstName, lastName, email, pass_hash, phoneNumber, verificationToken)
        VALUES (@FirstName, @LastName, @Email, @PassHash, @PhoneNumber, @VerificationToken)";

            var parameters = new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PassHash = hashedPassword,
                PhoneNumber = user.PhoneNumber,
                VerificationToken = verification
            };

            return _db.SaveData(sql, parameters);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public Task<UserModel?> GetUser(string email)
        {
            if (email == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty");

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format.");

            // Use parameterized query to insert user data
            string sql = @"
        SELECT TOP(1) * FROM dbo.[User] WHERE email = @Email";

            var parameters = new
            {
                Email = email,
            };

            return _db.LoadSingleRowAsync<UserModel, dynamic>(sql, parameters);
        }

        public Task VerifyUser(string email)
        {
            string sql =
@"
UPDATE dbo.[User]
SET emailVerified = '1'
WHERE email = @Email";

            var parameters = new
            {
                Email = email,
            };

            return _db.SaveData(sql, parameters);
        }

    }
}
