using System.Diagnostics.CodeAnalysis;

namespace WebApiProject.Models
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}