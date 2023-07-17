using WebApiProject.Models;

namespace WebApiProject.Repository
{
    public interface IMailRepository
    {
        bool SendEmail(MailRequest mailRequest);
    }
}