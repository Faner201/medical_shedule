namespace Entity;
public interface IUserRepository : IRepository<User>
{
    bool UserCheck(string login);

    User? GetUserByLogin(string login);

    User? CreateNewUser(User user);
}