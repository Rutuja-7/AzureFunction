using WebApiProject.Helpers;
using WebApiProject.Repository;
using WebApiProjectTest.MockData;

namespace WebApiProjectTest.Systems.Services
{
    public class UserServiceTest
    {
        [Fact]
        public void UserWithoutPassword()
        {
            ///arrange
            string username = "rutuja";
            string password = "test";
            var datasource = MockDataTest.UserDetails();
            var sut = new UserRepository();

            ///act
            var result = sut.Authenticate(username, password);

            ///assert
            result.Equals(datasource);
        }

        [Fact]
        public void UserNull()
        {
            ///arrange
            string? username = null;
            string? password = null;
            var datasource = MockDataTest.UserDetailsEmpty();
            var sut = new UserRepository();

            ///act
            var result = sut.Authenticate(username, password);

            ///assert
            result.Equals(datasource);
        }
    }
}