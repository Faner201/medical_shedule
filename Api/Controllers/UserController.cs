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
    public ActionResult UserCheck(string login)
    {
        var result = _service.UserCheck(login);

        if (result.Success)
        {
            return Ok();
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getUser")]
    public ActionResult<UserView> GetUserByLogin(string login)
    {
        var result = _service.GetUserByLogin(login);

        if (result.Success)
        {
            return Ok(new UserView(result.Value));
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }

    [HttpPost("createUser")]
    public ActionResult<UserView> CreateNewUser(User user)
    {
        var result = _service.CreateNewUser(user);

        if (result.Success)
        {
            return Ok(new UserView(result.Value));
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }
}