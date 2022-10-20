public class UserService 
{
    private  IUserRepository _db;

    public UserService(IUserRepository db)
    {
        _db = db;
    }
    public  Result<User> CreateNewUser(string login, string password)
    {
        if(string.IsNullOrEmpty(login))
            return Result.Fail<User>("Login reading error");

        if(string.IsNullOrEmpty(password))
            return Result.Fail<User>("Password reading error");

        if(_db.GetUserByLogin(login) is not null)
            return Result.Fail<User>("This login is already occupied");

        User user = new User(
            id: default,
            phoneNumber: "",
            name: "",
            role: new AccountRole(0),
            password: password,
            login: login
        );

        bool isCreate = _db.CreateNewUser(login, password);

        return isCreate ? Result.Ok<User>(user) : Result.Fail<User>("User not created");
    }

    public  Result<bool> UserCheck(string login)
    {
        if(string.IsNullOrEmpty(login))
            return Result.Fail<bool>("Login reading error");
            
        bool request = _db.UserCheck(login);

        return request ? Result.Ok<bool>(request) : Result.Fail<bool>("User not found");
    }

    public  Result<User> GetUserByLogin(string login)
    {
        if(string.IsNullOrEmpty(login))
            return Result.Fail<User>("Login reading error");

        User? request = _db.GetUserByLogin(login);

        return request is null ? Result.Fail<User>("User not found") : Result.Ok<User>(request);
    }
}