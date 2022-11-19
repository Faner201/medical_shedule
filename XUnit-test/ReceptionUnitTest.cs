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
    public void RecordCreationEmptySpecialization_ShouldFail()
    {
        int userId = 5;
        int doctorId = 10;
        DateTime date = new DateTime(1, 1, 1);
        Specialization specialization = new Specialization(5, string.Empty);

        var record = new Reception(doctorId, userId, date, date, specialization);

        var res = _receptionService.RecordCreation(record);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("There is no specialization", received);
    }

    [Fact]
    public void RecordCreationRecordExists_ShouldFail()
    {
        int userId = 5;
        int doctorId = 10;
        DateTime date = new DateTime(1, 1, 1);
        Specialization specialization = new Specialization(5, "Gigi");

        var record = new Reception(doctorId, userId, date, date, specialization);

        _receptionRepositoryMock.Setup(repository => repository.RecordExists(record))
            .Returns(() => true);

        var res = _receptionService.RecordCreation(record);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("There is already an appointment with this doctor for the selected date", received);
    }

    [Fact]
    public void RecordCreationOk_ShouldOk()
    {
        int userId = 5;
        int doctorId = 10;
        DateTime date = new DateTime(1, 1, 1);
        Specialization specialization = new Specialization(5, "Gigi");

        var record = new Reception(doctorId, userId, date, date, specialization);

        _receptionRepositoryMock.Setup(repository => repository.RecordExists(record))
            .Returns(() => false);

        _receptionRepositoryMock.Setup(repository => repository.RecordCreation(record))
            .Returns(() => new Reception(doctorId, userId, date, date, specialization));

        var res = _receptionService.RecordCreation(record);

        Assert.True(res.Success);
        Assert.Equal(string.Empty, res.Error);
    }

    [Fact]
    public void RecordCreationOtherError_ShouldFail()
    {
        int userId = 5;
        int doctorId = 10;
        DateTime date = new DateTime(1, 1, 1);
        Specialization specialization = new Specialization(5, "Gigi");

        var record = new Reception(doctorId, userId, date, date, specialization);

        _receptionRepositoryMock.Setup(repository => repository.RecordExists(record))
            .Returns(() => false);

        _receptionRepositoryMock.Setup(repository => repository.RecordCreation(record))
            .Returns(() => null);

        var res = _receptionService.RecordCreation(record);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("Failed to create appointment", received);
    }

    [Fact]
    public void GetAllFreeDatesEmptySpecialization_ShouldFail()
    {
        int specializationId = 4;
        DateOnly date = new DateOnly(2022, 11, 19);

        Specialization specialization = new Specialization(specializationId, string.Empty);

        var res = _receptionService.GetAllFreeDates(specialization, date);
        var received = res.Error;

        Assert.True(res.IsFail);
        Assert.Equal("There is no specialization", received);
    }

    [Fact]
    public void GetAllFreeDatesWithEmptyReception_ShouldOk()
    {
        int specializationId = 4;
        DateOnly date = new DateOnly(2022, 11, 19);

        Specialization specialization = new Specialization(specializationId, "One");
        
        DateTime start = date.ToDateTime(new TimeOnly(0, 0, 0));
        DateTime end = date.ToDateTime(new TimeOnly(23, 59, 59));

        var allFreeDates = new List<(DateTime, DateTime)>{(start, end)};

        _receptionRepositoryMock.Setup(repository => repository.GetAllDates(specialization, date))
            .Returns(() => new List<(DateTime, DateTime)>());

        var res = _receptionService.GetAllFreeDates(specialization, date);

        Assert.True(res.Success);
        Assert.Equal(string.Empty, res.Error);
        Assert.Equal(allFreeDates, res.Value);
    }

    [Fact]

    public void GetAllFreeDatesWithOneReception_ShouldOk()
    {
        int specializationId = 4;
        DateOnly date = new DateOnly(2022, 11, 19);

        Specialization specialization = new Specialization(specializationId, "One");
        
        DateTime start = date.ToDateTime(new TimeOnly(0, 0, 0));
        DateTime end = date.ToDateTime(new TimeOnly(23, 59, 59));

        var allFreeDates = new List<(DateTime, DateTime)>
            {
                (start, start.AddHours(18).AddMinutes(32)),
                (start.AddHours(20).AddMinutes(30), end)
            };

        _receptionRepositoryMock.Setup(repository => repository.GetAllDates(specialization, date))
            .Returns(() => new List<(DateTime, DateTime)>
                {
                    (start.AddHours(18).AddMinutes(32), start.AddHours(20).AddMinutes(30))
                });

        var res = _receptionService.GetAllFreeDates(specialization, date);

        Assert.True(res.Success);
        Assert.Equal(string.Empty, res.Error);
        Assert.Equal(allFreeDates, res.Value);

    }

    [Fact]
    public void GetAllFreeDatesWithMoryReception_ShouldOk()
    {
        int specializationId = 4;
        DateOnly date = new DateOnly(2022, 11, 19);

        Specialization specialization = new Specialization(specializationId, "One");
        
        DateTime start = date.ToDateTime(new TimeOnly(0, 0, 0));
        DateTime end = date.ToDateTime(new TimeOnly(23, 59, 59));

        var allFreeDates = new List<(DateTime, DateTime)>
            {
                (start, start.AddHours(8).AddMinutes(30)),
                (start.AddHours(10).AddMinutes(30), start.AddHours(12).AddMinutes(50))
            };

        _receptionRepositoryMock.Setup(repository => repository.GetAllDates(specialization, date))
            .Returns(() => new List<(DateTime, DateTime)>
                {
                    (start.AddHours(8).AddMinutes(30), start.AddHours(10).AddMinutes(30)),
                    (start.AddHours(12).AddMinutes(50), start.AddHours(23).AddMinutes(59).AddSeconds(59))
                });

        var res = _receptionService.GetAllFreeDates(specialization, date);

        Assert.True(res.Success);
        Assert.Equal(string.Empty, res.Error);
        Assert.Equal(allFreeDates, res.Value);
    }
}