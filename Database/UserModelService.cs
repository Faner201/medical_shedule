using Microsoft.EntityFrameworkCore;
using Entity;

namespace Database;

public class UserModelService : IUserRepository
{
    private ApplicationContext _db;

    public UserModelService(ApplicationContext db)
    {
        _db = db;
    }
    
    async public Task<User?> CreateNewUser(User user)
    {
        var request = await _db.User.FirstOrDefaultAsync(u => u.Login == user.Login);

        if (request is not null)
            return null;

        await _db.User.AddAsync(
            new UserModel{
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Role = new AccountRoleModel(){
                    AccountRoleId = user.Role.RoleId
                },
                Password = user.Password,
                Login = user.Login
            }
        );

        await _db.SaveChangesAsync();

        return new User(
            request.Id,
            request.PhoneNumber,
            request.Name,
            new AccountRole(request.Role.AccountRoleId),
            request.Password,
            request.Login
        );
    }

    async public Task<bool> UserCheck(string login) 
    {
        var user = await _db.User.FirstOrDefaultAsync(u => u.Login == login);

        if (user is null)
            return true;
        else
            return false;
    }

    async public Task<User?> GetUserByLogin(string login)
    {
        var user = await _db.User.FirstOrDefaultAsync(u => u.Login == login);

        if (user is null)
            return null;

        return new User(
            user.Id,
            user.PhoneNumber,
            user.Name,
            new AccountRole(user.Role.AccountRoleId),
            user.Password,
            user.Login
        );
    }
}