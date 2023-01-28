using Microsoft.AspNetCore.Mvc;
using Entity;

namespace Api;

[ApiController]
[Router("[controller]")]
public class UserController : ControllerBase
{
    private readonly Ilogger<UserController> _logger;
    private readonly UserService _service;

    public UserController(Ilogger<UserController> logger, UserService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("checkUser")]
    async public Task<ActionResult> UserCheck(string login)
    {
        var result = await _service.UserCheck(login);

        if (result.Success)
        {
            return Ok();
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getUser")]
    async public Task<ActionResult<UserView>> GetUserByLogin(string login)
    {
        var result = await _service.GetUserByLogin(login);

        if (result.Success)
        {
            return Ok(new UserView(result.Value));
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }

    [HttpPost("createUser")]
    async public  Task<ActionResult<UserView>> CreateNewUser(User user)
    {
        var result = await _service.CreateNewUser(user);

        if (result.Success)
        {
            return Ok(new UserView(result.Value));
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }
}