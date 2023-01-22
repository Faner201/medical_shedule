using Microsoft.AspNetCore.Mvc;
using Entity;

namespace Api;

[ApiController]
[Router("[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly ILogger<ScheduleController> _logger;
    private readonly ScheduleService _service;

    public ScheduleController(ILogger<ScheduleController> logger, ScheduleService service) 
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("getSchedule")]
    public ActionResult<ScheduleView> GetDoctorScheduleByDate(int doctorID, DateTime date)
    {
        var result = _service.GetDoctorScheduleByDate(doctorID, date);

        if (result.Success)
        {
            return Ok(new ScheduleView(result.Value));
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }
    
    [HttpPost("addScgedule")]
    public ActionResult<ScheduleView> AddScheduleDoctor(Schedule schedule)
    {
        var result = _service.AddScheduleDoctor(schedule);

        if (result.Success)
        {
            return Ok(new ScheduleView(result.Value));
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }

    [HttpPost("editSchedule")]
    public ActionResult<ScheduleView> EditScheduleDoctor([FromQuery] Schedule actual, [FromQuery] Schedule recent)
    {
        var result = _service.EditScheduleDoctor(actual, recent);

        if (result.Success)
        {
            return Ok(new ScheduleView(result.Value));
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }
}