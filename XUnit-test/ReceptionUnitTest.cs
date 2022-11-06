using Xunit;
using Entity;

namespace XUnit;
public class ReceptionUnitTest
{
    private readonly ReceptionService _receptionService;
    private readonly Mock<IReceptionRepository> _receptionRepositoryMock;

    public ReceptionUnitTest()
    {
        _receptionRepositoryMock = new Mock<IReceptionRepository>();
        _receptionService = new ReceptionService(_receptionRepositoryMock.Object);
    }

    [Fact]
    public void SaveDoctorAppointmentWithEmptyNotFound_ShouldFail()
    {
        int doctorID = 3242;
        DateTime date = new DateTime(1, 1, 1);

        _receptionRepositoryMock.Setup(repository => repository.SaveDoctorAppointment(date, doctorID))
            .Returns(() => null);
        
        var res = _receptionService.SaveDoctorAppointment(date, doctorID);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("Sorry, this entry is already taken", received);
    }

    [Fact]
    public void SaveDoctorAppointmentSuccessfully_ShouldOk()
    {
        int doctorID = 423432;
        DateTime date = new DateTime(1, 1, 1);

        _receptionRepositoryMock.Setup(repository => repository.SaveDoctorAppointment(date, doctorID))
            .Returns(() => new Reception(date, date, default, default));
        
        var res = _receptionService.SaveDoctorAppointment(date, doctorID);
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }

    [Fact]
    public void SaveDoctorAppointmentWithNotFound_ShouldFail()
    {
        DateTime date = new DateTime(1, 1, 1);

        _receptionRepositoryMock.Setup(repository => repository.SaveDoctorAppointment(date))
            .Returns(() => null);
        
        var res = _receptionService.SaveDoctorAppointment(date);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("I'm sorry, I couldn't get an appointment", received);
    }

    [Fact]
    public void SaveDoctorAppointment_ShouldOk()
    {
        DateTime date = new DateTime(1, 1, 1);

        _receptionRepositoryMock.Setup(repository => repository.SaveDoctorAppointment(date))
            .Returns(() => new Reception(date, date, default, default));
        
        var res = _receptionService.SaveDoctorAppointment(date);
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }
    [Fact]
    public void GetFreeAppointmentDateListEmptySpecialization_ShouldFail()
    {
        Specialization specialization = new Specialization(default, "");

        var res = _receptionService.GetFreeAppointmentDateList(specialization);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("This specialty was not found", received);
    }

    [Fact]
    public void GetFreeAppointmentDateListWithEmptyNotFound_ShouldFail()
    {
        Specialization specialization = new Specialization(default, " ");

        _receptionRepositoryMock.Setup(repository => repository.GetFreeAppointmentDateList(specialization))
            .Returns(() => null);
        
        var res = _receptionService.GetFreeAppointmentDateList(specialization);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("There are no entries as of this date", received);
    }

    [Fact]
    public void GetFreeAppointmentDateList()
    {
        Specialization specialization = new Specialization(default, "Дифференциальный осмотр");

        _receptionRepositoryMock.Setup(repository => repository.GetFreeAppointmentDateList(specialization))
            .Returns(() => new List<DateTime>());

        var res = _receptionService.GetFreeAppointmentDateList(specialization);
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }
}