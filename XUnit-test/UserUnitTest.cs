using Xunit;
using Entity;

namespace XUnit;
public class UserUnitTest
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UserUnitTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public void CreateNewUserWithEmptyLogin_ShouldFail()
    {
        int id = 5;
        string phoneNumber = "3-435-343-43-22";
        string Name = "Faner201";
        string password = "gigachad password";

        User user = new User(id, phoneNumber, Name, new AccountRole(0), password, string.Empty);

        var res = _userService.CreateNewUser(user);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("Login reading error", received);
    }

    [Fact]
    public void CreateNewUserWithEmptyPassword_ShouldFail()
    {
        int id = 5;
        string phoneNumber = "3-435-343-43-22";
        string Name = "Faner201";
        string login = "faner201";

        User user = new User(id, phoneNumber, Name, new AccountRole(0), string.Empty, login);

        var res = _userService.CreateNewUser(user);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("Password reading error", received);
    }

    [Fact]
    public void CreateNewUserWithOccupiedLogin_ShouldFail()
    {
        int id = 5;
        string phoneNumber = "3-435-343-43-22";
        string Name = "Faner201";
        string login = "faner201";
        string password = "gigachad password";

        User user = new User(id, phoneNumber, Name, new AccountRole(0), password, login);
        
        _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
            .Returns(() => new User(1, "", "", new AccountRole(0), "", ""));
        
        var res = _userService.CreateNewUser(user);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("This login is already occupied", received);
    }

    [Fact]
    public void CreateNewUserSuccessfully_ShouldOk()
    {
        int id = 5;
        string phoneNumber = "3-435-343-43-22";
        string Name = "Faner201";
        string login = "faner201";
        string password = "gigachad password";

        User user = new User(id, phoneNumber, Name, new AccountRole(0), password, login);

        _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
            .Returns(() => null);

        _userRepositoryMock.Setup(repository => repository.CreateNewUser(It.IsAny<User>()))
            .Returns(() => new User(8, phoneNumber, Name, new AccountRole(0), password, login));

        var res = _userService.CreateNewUser(user);
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
        Assert.Equal(login, res.Value.Login);
        Assert.Equal(password, res.Value.Password);
    }

    [Fact]
    public void CreateNewUserAnotherErorr_ShouldFail()
    {
        int id = 5;
        string phoneNumber = "3-435-343-43-22";
        string name = "Faner201";
        string login = "faner201";
        string password = "gigachad password";

        User user = new User(id, phoneNumber, name, new AccountRole(0), password, login);

        _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
            .Returns(() => null);
        _userRepositoryMock.Setup(repository => repository.CreateNewUser(It.IsAny<User>()))
            .Returns(() => null);

        var res = _userService.CreateNewUser(user);
        var received = res.Error;
        
        Assert.True(res.IsFail);
        Assert.Equal("User not created", received);
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