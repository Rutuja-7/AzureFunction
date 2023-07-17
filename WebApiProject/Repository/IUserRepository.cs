using WebApiProject.Models;

namespace WebApiProject.Repository
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string username, string password);
    }
}