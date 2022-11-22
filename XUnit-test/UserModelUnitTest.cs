using Entity;
using Database;
using Microsoft.EntityFrameworkCore;

namespace XUnit;
public class UserModelUnitTest
{
    private readonly DbContextOptionsBuilder<ApplicationContext> _optionsBuilder;
    private readonly IUserRepository _userRepository;

    public UserModelUnitTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=Test_medical_shedule;Username=postgres;Password=Bkmz2309865"
        );
        _optionsBuilder = optionsBuilder;
        using var _context = new ApplicationContext(_optionsBuilder.Options);
        _userRepository = new UserModelService(_context);
    }

    [Fact]
    public void CreateNewUser()
    {
        string name = "Lopata";
        string phoneNumber = "89424278860";
        string login = "Abobick";
        string password = "fsdfwerw";
        AccountRoleModel role = new AccountRoleModel(5);

        var user = new User(1, phoneNumber, name, role, password, login);

        var res = _userRepository.CreateNewUser(user);

        Assert.True(res is not null);
        Assert.Equal(user.Id, res.Id);
        Assert.Equal(user.Role, res.Role);
        Assert.Equal(user.Name, res.Name);
        Assert.Equal(user.PhoneNumber, res.PhoneNumber);
        Assert.Equal(user.Password, res.Password);
        Assert.Equal(user.Login, res.Login);
    }

    [Fact]
    public void UserCheck()
    {
        string login = "Abp";

        var res = _userRepository.UserCheck(login);

        Assert.True(res.Success);
    }

    [Fact]
    public void GetUserByLogin()
    {
        string name = "Lopata";
        string phoneNumber = "89424278860";
        string login = "Abobick";
        string password = "fsdfwerw";
        AccountRoleModel role = new AccountRoleModel(5);

        var user = new User(1, phoneNumber, name, role, password, login);

        var res = _userRepository.GetUserByLogin(login);

        Assert.True(res is not null);
        Assert.Equal(user.Id, res.Id);
        Assert.Equal(user.Role, res.Role);
        Assert.Equal(user.Name, res.Name);
        Assert.Equal(user.PhoneNumber, res.PhoneNumber);
        Assert.Equal(user.Password, res.Password);
        Assert.Equal(user.Login, res.Login);
    }

}