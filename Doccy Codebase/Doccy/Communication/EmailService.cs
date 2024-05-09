using Azure;
using Azure.Communication.Email;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Doccy.Communication
{
    public class EmailService : IEmailService
    {
        EmailClient emailClient = new EmailClient("Endpoint here"); //Removed for Git

        public void SendEmail(string sender, string recipient, List<DocumentModel> documentList)
        {

            // Create the email content
            var emailContent = new EmailContent("Doccy Document Share")
            {
                PlainText = "This email message is sent from Doccy.",
                Html = "<html><body><h1>You have recieved " + documentList.Count.ToString() + " files from " + sender + ". </h1><br/><h4>This is an encrypted email with attatchments</h4><p>Visit doccy.azurewebsites.net to store and share your personal files.</p></body></html>"
            };

            // Create the EmailMessage
            var emailMessage = new EmailMessage(
                senderAddress: "DoNotReply@330ec8d9-dfca-460a-9bfd-245435333f2a.azurecomm.net",
                recipientAddress: recipient,
                content: emailContent);

            AddEmailAttachments(documentList, emailMessage);

            try
            {
                EmailSendOperation emailSendOperation = emailClient.Send(WaitUntil.Completed, emailMessage);

            }
            catch (RequestFailedException ex)
            {
                // Handle exception
                Console.WriteLine($"Email send operation failed with error code: {ex.ErrorCode}, message: {ex.Message}");
            }

        }

        public void SendVerificationEmail(string user, string verificationCode)
        {

            // Create the email content
            var emailContent = new EmailContent("Doccy Email Verification")
            {
                PlainText = "This email message is sent from Doccy.",
                Html = "<html><body><h1>" + verificationCode + "</h1><br/><h4>Please enter the above code on the Doccy site to verify your email!</h4><p>doccy.azurewebsites.net/Verification</p></body></html>"
            };

            // Create the EmailMessage
            var emailMessage = new EmailMessage(
                senderAddress: "DoNotReply@330ec8d9-dfca-460a-9bfd-245435333f2a.azurecomm.net", 
                recipientAddress: user,
                content: emailContent);

            try
            {
                EmailSendOperation emailSendOperation = emailClient.Send(WaitUntil.Completed, emailMessage);

            }
            catch (RequestFailedException ex)
            {
                // Handle exception
                Console.WriteLine($"Email send operation failed with error code: {ex.ErrorCode}, message: {ex.Message}");
            }
        }



        private void AddEmailAttachments(List<DocumentModel> documentList, EmailMessage emailMessage)
        {
            foreach (DocumentModel document in documentList)
            {
                string? extension = Path.GetExtension(document.DocumentTitle)?.ToLower();

                string mediaType;

                switch (extension)
                {
                    case ".pdf":
                        mediaType = MediaTypeNames.Application.Pdf;
                        break;
                    case ".jpeg":
                    case ".jpg":
                        mediaType = MediaTypeNames.Image.Jpeg;
                        break;
                    case ".png":
                        mediaType = MediaTypeNames.Image.Png;
                        break;
                    default:
                        continue;
                }

                var contentBinaryData = new BinaryData(document.Data);
                emailMessage.Attachments.Add(new EmailAttachment(document.DocumentTitle, mediaType, contentBinaryData));
            }
        }
    }
}
