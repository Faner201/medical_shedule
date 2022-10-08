using Xunit;

    public class UnitTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UnitTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public void CreateNewUserWithEmptyLogin_ShouldFail()
        {
            string login = string.Empty;
            string password = string.Empty;

            var res = _userService.CreateNewUser(login, password);
            var received = res.Error;

            Assert.True(res.IsFail);
            Assert.Equal("Login reading error", received);
        }

        [Fact]
        public void CreateNewUserWithEmptyPassword_ShouldFail()
        {
            string login = "faner201";
            string password = string.Empty;

            var res = _userService.CreateNewUser(login, password);
            var received = res.Error;

            Assert.True(res.IsFail);
            Assert.Equal("Password reading error", received);
        }

        [Fact]
        public void CreateNewUserWithOccupiedLogin_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
                .Returns(() => new User(1, "", "", new AccountRole(0), "", ""));

            string login = "faner201";
            string password = "password";
            
            var res = _userService.CreateNewUser(login, password);
            var received = res.Error;

            Assert.True(res.IsFail);
            Assert.Equal("This login is already occupied", received);
        }

        [Fact]
        public void CreateNewUserSuccessfully_ShouldOk()
        {
            _userRepositoryMock.Setup(repository => repository.CreateNewUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => true);

            string login = "faner201";
            string password = "password";

            var res = _userService.CreateNewUser(login, password);
            var received = res.Error;

            Assert.True(res.Success);
            Assert.Equal(string.Empty, received);
            Assert.Equal(login, res.Value.Login);
            Assert.Equal(password, res.Value.Password);
        }

        [Fact]
        public void CreateNewUserAnotherErorr_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.CreateNewUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => false);

            string login = "faner201";
            string password = "password";

            var res = _userService.CreateNewUser(login, password);
            var received = res.Error;
            
            Assert.True(res.IsFail);
            Assert.Equal("User not created", received);
            Assert.Equal(null, res.Value);
        }

        [Fact]
        public void UserCheckWithEmptyLogin_ShouldFail()
        {
            string login = string.Empty;

            var res =_userService.UserCheck(login);
            var received = res.Error;

            Assert.True(res.IsFail);
            Assert.Equal("Login reading error", received);
        }
        [Fact]
        public void UserCheckSuccessfully_ShouldOk()
        {
           _userRepositoryMock.Setup(repository => repository.UserCheck(It.IsAny<string>()))
                .Returns(() => true);

            string login = "faner201";

            var res = _userService.UserCheck(login);
            var received = res.Error;

            Assert.True(res.Success);
            Assert.Equal(string.Empty, received);
        }

        [Fact]
        public void UserCheckAnoutherError_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.UserCheck(It.IsAny<string>()))
                .Returns(() => false);

            string login = "faner201";

            var res = _userService.UserCheck(login);
            var received = res.Error;

            Assert.True(res.IsFail);
            Assert.Equal("User not found", received);
        }

        [Fact]
        public void GetUserByLoginEmptyLogin_ShouldFail()
        {
           string login = string.Empty;

           var res = _userService.GetUserByLogin(login);
           var received = res.Error;

           Assert.True(res.IsFail);
           Assert.Equal("Login reading error", received);
        }

        [Fact]
        public void GetUserByLoginSuccessfully_ShouldOk()
        {
            string login = "faner201";

            _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
                .Returns(() => new User(default, string.Empty, string.Empty, new AccountRole(0), string.Empty, login));
            
            var res = _userService.GetUserByLogin(login);
            var received = res.Error;

            Assert.True(res.Success);
            Assert.Equal(string.Empty, received);
            Assert.Equal(login, res.Value.Login);
        }

        [Fact]
        public void GetUserByLoginUnknownError_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
                .Returns(() => null);

            string login = "faner201";
            
            var res = _userService.GetUserByLogin(login);
            var received = res.Error;

            Assert.True(res.IsFail);
            Assert.Equal("User not found", received);
        }
    }