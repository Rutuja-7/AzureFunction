using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApiProject.Models
{
    [ExcludeFromCodeCoverage]
    public class MailRequest
    {
        [Required]
        [EmailAddress]
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}