namespace Entity;
public interface IUserRepository
{
    bool UserCheck(string login);

    User? GetUserByLogin(string login);

    User? CreateNewUser(User user);
}