using Microsoft.AspNetCore.Mvc;
using Entity;

namespace Api;

[ApiController]
[Router("[controller]")]
public class ReceptionController: ControllerBase
{
    private readonly ILogger<ScheduleController> _logger;
    private readonly ReceptionService _service;

    public ReceptionController(ILogger<ReceptionController> logger, ReceptionService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("createReception")]
    async public Task<ActionResult<ReceptionView>> RecordCreation(Reception reception)
    {
        var result = await _service.RecordCreation(reception);

        if (result.Success)
        {
            return Ok(new ReceptionView(result.Value));
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getFreeDates")]
    async public Task<ActionResult<List<(DateTime, DateTime)>>> GetAllDates(Specialization specialization, DateOnly date)
    {
        var result = await _service.GetAllDates(specialization, date);

        if (result.Success)
        {
            return Ok(result.Value);
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }
}