namespace Entity;
public interface IUserRepository
{
    Task<bool> UserCheck(string login);

    Task<User?> GetUserByLogin(string login);

    Task<User?> CreateNewUser(User user);
}