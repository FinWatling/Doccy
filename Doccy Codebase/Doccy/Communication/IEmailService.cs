using DataAccessLibrary.Models;

namespace Doccy.Communication
{
    public interface IEmailService
    {
        void SendEmail(string sender, string recipient, List<DocumentModel> documentList);
        void SendVerificationEmail(string user, string verificationCode);
    }
}