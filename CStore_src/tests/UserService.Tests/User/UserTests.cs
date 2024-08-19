using User.API.Users.CreateUser;
using User.API.Users.UserLogin;
using UserService.Tests.Abstractions;

namespace UserService.Tests.User
{
    public class UserTests : BaseIntegrationTest
    {
        public UserTests(IntegrationTestWebAppFactory factory) : base(factory) 
        {

        }

        [Fact]
        public async Task Create_TrueCreateUser()
        {
            var command = new CreateUserCommand
            (
                "test1@gmail.com",
                "1232003",
                "credentials",
                "User"
            );
            var result = await Sender.Send(command);
            Assert.True(result.Status);
        }

        [Fact]
        public async Task Login_UserLoggedSuccessfully()
        {
            var query = new UserLoginQuery
            (
                "tn0888888@gmail.com",
                "1232003"
            );

            var result = await Sender.Send(query);
            Assert.NotNull(result.User);
        }
    }
}

