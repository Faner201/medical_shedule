public class UserService 
{
    private static IUserRepository db = new SQLUserRepository();
    public static Result<User> CreateNewUser(string login, string password)
    {
        if(string.IsNullOrEmpty(login))
            return Result.Fail<User>("Login reading error");

        if(string.IsNullOrEmpty(password))
            return Result.Fail<User>("Password reading error");

        if(db.GetUserByLogin(login) is not null)
            return Result.Fail<User>("This login is already occupied");

        User user = new User(
            id: default,
            phoneNumber: default,
            name: string.Empty,
            role: new AccountRole(0),
            password: password,
            login: login
        );

        bool isCreate = db.CreateNewUser(login, password);

        return isCreate ? Result.Ok<User>(user) : Result.Fail<User>("User not found");
    }

    public static Result UserCheck(string login, string password)
    {
        if(string.IsNullOrEmpty(login))
            return Result.Fail("Login reading error");

        if(string.IsNullOrEmpty(password))
            return Result.Fail("Password reading error");
            
        bool request = db.UserCheck(login, password);

        return request ? Result.Ok() : Result.Fail("User not found");
    }

    public static Result<User> GetUserByLogin(string login)
    {
        if(string.IsNullOrEmpty(login))
            return Result.Fail<User>("Login reading error");

        User? request = db.GetUserByLogin(login);

        return request is null ? Result.Fail<User>("User not found") : Result.Ok<User>(request);
    }
}