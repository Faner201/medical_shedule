using Xunit;

public class ScheduleUnitTest
{
    private readonly ScheduleService _scheduleService;
    private readonly Mock<IScheduleRepository> _scheduleRepositoryMock;

    public ScheduleUnitTest()
    {
        _scheduleRepositoryMock = new Mock<IScheduleRepository>();
        _scheduleService = new ScheduleService(_scheduleRepositoryMock.Object);
    }

    [Fact]
    public void GetDoctorScheduleByDateWithEmptyNotFound_ShouldFail()
    {
        int doctorID = 4324;
        DateTime date = new DateTime(1, 1, 1);

        _scheduleRepositoryMock.Setup(repository => repository.GetDoctorScheduleByDate(doctorID, date))
            .Returns(() => null);

        var res = _scheduleService.GetDoctorScheduleByDate(doctorID, date);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("The schedule for this doctor was not found ", received);
    }

    [Fact]
    public void GetDoctorScheduleByDateSuccessfully_ShouldOk()
    {
        int doctorID = 4234;
        DateTime date = new DateTime(1, 1, 1);

        _scheduleRepositoryMock.Setup(repository => repository.GetDoctorScheduleByDate(doctorID, date))
            .Returns(() => new Schedule(default, date, date));

        var res = _scheduleService.GetDoctorScheduleByDate(doctorID, date);
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }

    [Fact]
    public void AddScheduleDoctorWithEmptyNotFound_ShouldFail()
    {
        DateTime date = new DateTime(1, 1, 1);
        Schedule schedule = new Schedule(default, date, date);

        _scheduleRepositoryMock.Setup(repository => repository.AddScheduleDoctor(schedule))
            .Returns(() => null);

        var res = _scheduleService.AddScheduleDoctor(schedule);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("I couldn't add the schedule", received);
    }

    [Fact]
    public void AddScheduleDoctorSuccessfully_ShouldOk()
    {
        DateTime date = new DateTime(1, 1, 1);
        Schedule schedule = new Schedule(default, date, date);

        _scheduleRepositoryMock.Setup(repository => repository.AddScheduleDoctor(schedule))
            .Returns(() => new Schedule(default, date, date));
        
        var res = _scheduleService.AddScheduleDoctor(schedule);
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }

    [Fact]
    public void EditScheduleDoctorWithEmptyNotFound_ShouldFail()
    {
        DateTime date = new DateTime(1, 1, 1);
        Schedule schedule = new Schedule(default, date, date);

        _scheduleRepositoryMock.Setup(repository => repository.EditScheduleDoctor(schedule))
            .Returns(() => null);
        
        var res = _scheduleService.EditScheduleDoctor(schedule);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("It was not possible to add changes to the schedule", received);
    }

    [Fact]
    public void EditScheduleDoctorSuccessfully_ShouldOk()
    {
       DateTime date = new DateTime(1, 1, 1);
       Schedule schedule = new Schedule(default, date, date);

        _scheduleRepositoryMock.Setup(repository => repository.EditScheduleDoctor(schedule))
            .Returns(() => new Schedule(default, date, date));

        var res = _scheduleService.EditScheduleDoctor(schedule);
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }
}