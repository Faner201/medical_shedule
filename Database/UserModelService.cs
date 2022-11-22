using Entity;

namespace Database;

public class UserModelService : IUserRepository
{
    private ApplicationContext _db;

    public UserModelService(ApplicationContext db)
    {
        _db = db;
    }
    
    public User? CreateNewUser(User user)
    {
        var request = _db.User.FirstOrDefault(u => u.Login == user.Login);

        if (request is not null)
            return null;

        _db.User.Add(
            new UserModel{
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Role = user.Role,
                Password = user.Password,
                Login = user.Login
            }
        );

        _db.SaveChanges();

        return new User(
            request.Id,
            request.PhoneNumber,
            request.Name,
            request.Role,
            request.Password,
            request.Login
        );
    }

    public bool UserCheck(string login) 
    {
        var user = _db.User.FirstOrDefault(u => u.Login == login);

        if (user is null)
            return true;
        else
            return false;
    }

    public User? GetUserByLogin(string login)
    {
        var user = _db.User.FirstOrDefault(u => u.Login == login);

        if (user is null)
            return null;

        return new User(
            user.Id,
            user.PhoneNumber,
            user.Name,
            user.Role,
            user.Password,
            user.Login
        );
    }
}