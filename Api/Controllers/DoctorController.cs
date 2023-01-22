using Microsoft.AspNetCore.Mvc;
using Entity;

namespace Api;

[ApiController]
[Router("[controller]")]
public class DoctorController :  ControllerBase
{
    private readonly ILogger<DoctorController> _logger;
    private readonly DoctorService _service;

    public DoctorController(ILogger<DoctorController> logger, DoctorService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("createDoctor")]
    public ActionResult<DoctorView> CreateNewDoctor(Doctor doctor)
    {
        var result = _service.CreateNewDoctor(doctor);
        
        if (result.Success) {
            return Ok();
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }

    [HttpPost("deleteDoctor")]
    public ActionResult DeleteDoctor(int doctorID)
    {
        var result = _service.DeleteDoctor(doctorID);

        if (result.Success)
        {
            return Ok();
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getDoctorList")]
    public ActionResult<List<DoctorView>> GetDoctorList()
    {
        var result = _service.GetDoctorList();

        if (result.Success)
        {
            return Ok(result.Value.ConvertAll(doctor => new DoctorView(doctor)));
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getDoctor")]
    public ActionResult<DoctorView> GetDoctor(int doctorID)
    {
        var result = _service.GetDoctor(doctorID);

        if (result.Success)
        {
            return Ok(new DoctorView(result.Value));
        }

        return Problem(statusCode:(int)ErrorCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getDoctors")]
    public ActionResult<List<DoctorView>> GetDoctors(Specialization specialization)
    {
        var result = _service.GetDoctors(specialization);

        if (result.Success)
        {
            return Ok(result.Value.ConvertAll(doctor => new DoctorView(doctor)));
        }
    }
}