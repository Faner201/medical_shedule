using Entity;
using Database;
using Microsoft.EntityFrameworkCore;
namespace XUnit;

public class ScheduleModelUnitTest
{
    private readonly DbContextOptionsBuilder<ApplicationContext> _optionsBuilder;
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleModel()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=Test_medical_shedule;Username=postgres;Password=Bkmz2309865"
        );
        _optionsBuilder = optionsBuilder;
        using var _context = new ApplicationContext(_optionsBuilder.Options);
        _scheduleRepository = new ScheduleModelService(_context);
    }

    [Fact]
    public void AddScheduleDoctor()
    {
        DateTime start = new DateTime(1, 1, 1, 1, 1);
        DateTime end = new DateTime(1, 1, 1, 1, 1);
        int doctorId = 1;

        var schedule = new Schedule(doctorId, start, end);
        
        var res = _scheduleRepository.AddScheduleDoctor(schedule);

        Assert.True(res is not null);
        Assert.Equal(schedule.IdDoctor, res.IdDoctor);
        Assert.Equal(schedule.Start, res.Start);
        Assert.Equal(schedule.End, res.End);
    }

    [Fact]
    public void GetDoctorScheduleByDate()
    {
        DateTime date = new DateTime(1, 1, 1, 1, 1);
        DateTime start = new DateTime(1, 1, 1, 1, 1);
        DateTime end = new DateTime(1, 1, 1, 1, 1);
        int doctorId = 1;

        var schedule = new Schedule(doctorId, start, end);

        var res = _scheduleRepository.GetDoctorScheduleByDate(doctorId, date);

        Assert.True(res is not null);
        Assert.Equal(schedule.IdDoctor, res.IdDoctor);
        Assert.Equal(schedule.Start, res.Start);
        Assert.Equal(schedule.End, res.End);
    }

    [Fact]
    public void EditScheduleDoctor()
    {
        DateTime start = new DateTime(1, 1, 1, 1, 1);
        DateTime end = new DateTime(1, 1, 1, 1, 1);
        int doctorId = 1;

        var actual = new Schedule(doctorId, start, end);

        DateTime startEdit = new DateTime(2, 2, 2, 2, 2);
        DateTime endEdit = new DateTime(2, 2, 2, 2, 2);

        var recent = new Schedule(doctorId, startEdit, endEdit);

        var res = _scheduleRepository.EditScheduleDoctor(actual, recent);

        Assert.True(res is not null);
        Assert.Equal(recent.IdDoctor, res.IdDoctor);
        Assert.Equal(recent.Start, res.Start);
        Assert.Equal(recent.End, res.End);
    }
}