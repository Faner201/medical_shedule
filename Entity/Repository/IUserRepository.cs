public interface IUserRepository : IRepository<User>
{
    bool UserCheck(string login);

    User GetUserByLogin(string login);

    bool CreateNewUser(string login, string password);
}