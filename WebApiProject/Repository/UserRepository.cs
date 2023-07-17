using WebApiProject.Helpers;
using WebApiProject.Models;

namespace WebApiProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users = new List<User>
        {
            new User {Username="rutuja", Password="test"}
        };

        /// <summary>
        /// This method is used to authenticate the user with available users
        /// </summary>
        /// <param name="username", name="password"></param>
        /// <returns>Task<User></returns>
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));
            // return null if user not found
            if (user == null)
                return null;
            // authentication successful so return user details without password
            return user.WithoutPassword();
        }
    }
}