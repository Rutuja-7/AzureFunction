using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using WebApiProject.Models;

namespace WebApiProject.Repository
{
    public class MailRepository : IMailRepository
    {
        private readonly MailSettings _mailSettings;
        public MailRepository(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        /// <summary>
        /// This method is used to send the mail
        /// </summary>
        /// <param name="mailRequest"></param>
        /// <returns></returns>
        public bool SendEmail(MailRequest mailRequest)
        {
            if (mailRequest.ToEmail == null || mailRequest.ToEmail == "")
            {
                return false;
            }
            else
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Username);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Username, _mailSettings.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
        }
    }
}