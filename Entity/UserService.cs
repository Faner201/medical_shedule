namespace Entity;
public class UserService 
{
    private  IUserRepository _userService;
    private static SemaphoreSlim userSemaphore = new SemaphoreSlim(1, 1);

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

         User? isCreate = null;

        try
        {
            await userSemaphore.WaitAsync();

            isCreate = await _userService.CreateNewUser(user);
        }
        finally
        {
            userSemaphore.Release();
        }

        return isCreate is not null ? Result.Ok<User>(user) : Result.Fail<User>("User not created");
    }

    async public  Task<Result<bool>> UserCheck(string login)
    {
        if(string.IsNullOrEmpty(login))
            return Result.Fail<bool>("Login reading error");
            
        Boolean request = false;

        try {
            await userSemaphore.WaitAsync();

            request = await _userService.UserCheck(login);
        }
        finally
        {
            userSemaphore.Release();
        }

        return request ? Result.Ok<bool>(request) : Result.Fail<bool>("User not found");
    }

    async public  Task<Result<User>> GetUserByLogin(string login)
    {
        if(string.IsNullOrEmpty(login))
            return Result.Fail<User>("Login reading error");

        User? request = null;

        try
        {
            await userSemaphore.WaitAsync();

            request = await _userService.GetUserByLogin(login);
        }
        finally
        {
            userSemaphore.Release();
        }

        return request is null ? Result.Fail<User>("User not found") : Result.Ok<User>(request);
    }
}