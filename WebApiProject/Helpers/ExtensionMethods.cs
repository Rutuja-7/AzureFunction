using WebApiProject.Models;

namespace WebApiProject.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// This method will return a user without password
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User</returns>
        public static User WithoutPassword(this User user) {
            user.Password = null;
            return user;
        }
    }
}