namespace Database;

using Entity;

public class UserModelService : IUserRepository
{
    private ApplicationContext _db;

    public UserModelService(ApplicationContext db)
    {
        _db = db;
    }
    
    public User CreateNewUser(User user)
    {
        _db.User.Add(new UserModel{
            PhoneNumber = user.PhoneNumber,
            Name = user.Name,
            Role = user.Role,
            Password = user.Password,
            Login = user.Login
        });
        _db.SaveChanges();
    }

    public bool UserCheck(string login) 
    {
        if (_db.User.FirstOrDefault(u => u.Login == login))
        {
            return true;
        }
        return false;
    }

    public User GetUserByLogin(string login)
    {
        var request = _db.User.FirstOrDefault(u => u.Login == login);

        return new User{
            Id = request.Id,
            PhoneNumber = request.PhoneNumber,
            Name = request.Name,
            Role = request.Role,
            Password = request.Password,
            login = request.Login
        };
    }
}