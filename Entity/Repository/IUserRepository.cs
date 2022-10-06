interface IUserRepository : IRepository<User>
{
    bool UserCheck(string login, string password);

    User GetUserByLogin(string login);

    bool CreateNewUser(string login, string password);
}