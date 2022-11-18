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
        int userID = 5235;
        DateTime date = new DateTime(1, 1, 1);

        var reception = new Reception(date, date, userID, doctorID);

        _receptionRepositoryMock.Setup(repository => repository.SaveDoctorAppointment(reception))
            .Returns(() => null);
        
        var res = _receptionService.SaveDoctorAppointment(reception);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("Sorry, this entry is already taken", received);
    }

    [Fact]
    public void SaveDoctorAppointmentSuccessfully_ShouldOk()
    {
        int doctorID = 3242;
        int userID = 5235;
        DateTime date = new DateTime(1, 1, 1);

        var reception = new Reception(date, date, userID, doctorID);

        _receptionRepositoryMock.Setup(repository => repository.SaveDoctorAppointment(reception))
            .Returns(() => new Reception(date, date, default, default));
        
        var res = _receptionService.SaveDoctorAppointment(reception);
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }

    [Fact]
    public void SaveAnyFreeDoctorAppointmentWithNotFound_ShouldFail()
    {
        int doctorID = 3242;
        int userID = 5235;
        DateTime date = new DateTime(1, 1, 1);

        var reception = new Reception(date, date, userID, doctorID);

        _receptionRepositoryMock.Setup(repository => repository.SaveAnyFreeDoctorAppointment(reception))
            .Returns(() => null);
        
        var res = _receptionService.SaveAnyFreeDoctorAppointment(reception);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("I'm sorry, I couldn't get an appointment", received);
    }

    [Fact]
    public void SaveAnyFreeDoctorAppointment_ShouldOk()
    {
        int doctorID = 3242;
        int userID = 5235;
        DateTime date = new DateTime(1, 1, 1);

        var reception = new Reception(date, date, userID, doctorID);

        _receptionRepositoryMock.Setup(repository => repository.SaveAnyFreeDoctorAppointment(reception))
            .Returns(() => new Reception(date, date, default, default));
        
        var res = _receptionService.SaveAnyFreeDoctorAppointment(reception);
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }
    [Fact]
    public void GetFreeAppointmentDateListEmptySpecialization_ShouldFail()
    {
        Specialization specialization = new Specialization(default, "");
        DateOnly date = new DateOnly(234,532,623);

        var res = _receptionService.GetFreeAppointmentDateList(specialization, date);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("This specialty was not found", received);
    }

    [Fact]
    public void GetFreeAppointmentDateListWithEmptyNotFound_ShouldFail()
    {
        Specialization specialization = new Specialization(default, "");
        DateOnly date = new DateOnly(234,532,623);

        _receptionRepositoryMock.Setup(repository => repository.GetFreeAppointmentDateList(specialization, date))
            .Returns(() => null);
        
        var res = _receptionService.GetFreeAppointmentDateList(specialization, date);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("There are no entries as of this date", received);
    }

    [Fact]
    public void GetFreeAppointmentDateList()
    {
        Specialization specialization = new Specialization(default, "Дифференциальный осмотр");
        DateOnly date = new DateOnly(234,532,623);
        var start = date.ToDateTime(new TimeOnly(0, 0, 0));
        var end = date.ToDateTime(new TimeOnly(23, 59, 59));
        var listDates = new List<(DateTime, DateTime)>{(start, end)};

        _receptionRepositoryMock.Setup(repository => repository.GetFreeAppointmentDateList(specialization,date))
            .Returns(() => new List<(DateTime,DateTime)>());

        var res = _receptionService.GetFreeAppointmentDateList(specialization,date);
        var received = res.Error;

        Assert.True(res.Success);
        Assert.Equal(string.Empty, received);
    }
}