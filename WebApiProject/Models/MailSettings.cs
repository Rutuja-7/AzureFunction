using System.Diagnostics.CodeAnalysis;

namespace WebApiProject.Models
{
    [ExcludeFromCodeCoverage]
    public class MailSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}