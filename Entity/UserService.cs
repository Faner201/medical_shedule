namespace Entity;
public class UserService 
{
    private  IUserRepository _userService;
    public UserService(IUserRepository userService)
    {
        _userService = userService;
    }
    async public  Task<Result<User>> CreateNewUser(User user)
    {
        if(string.IsNullOrEmpty(user.Login))
            return Result.Fail<User>("Login reading error");

        if(string.IsNullOrEmpty(user.Password))
            return Result.Fail<User>("Password reading error");

        if(_userService.GetUserByLogin(user.Login) is not null)
            return Result.Fail<User>("This login is already occupied");

        var isCreate = await _userService.CreateNewUser(user);

        return isCreate is not null ? Result.Ok<User>(user) : Result.Fail<User>("User not created");
    }

    async public  Task<Result<bool>> UserCheck(string login)
    {
        if(string.IsNullOrEmpty(login))
            return Result.Fail<bool>("Login reading error");
            
        var request = await _userService.UserCheck(login);

        return request ? Result.Ok<bool>(request) : Result.Fail<bool>("User not found");
    }

    async public  Task<Result<User>> GetUserByLogin(string login)
    {
        if(string.IsNullOrEmpty(login))
            return Result.Fail<User>("Login reading error");

        var request = await _userService.GetUserByLogin(login);

        return request is null ? Result.Fail<User>("User not found") : Result.Ok<User>(request);
    }
}