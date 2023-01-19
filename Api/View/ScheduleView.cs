using System.Text.Json.Serialization;
using Entity;

namespace Api;

public class ScheduleView
{
    [JsonPropertyName("doctorID")]
    public int DoctorID { get; set; }
    [JsonPropertyName("start")]
    public DateTime Start { get; set; }
    [JsonPropertyName("end")]
    public DateTime End { get; set; }

    public ScheduleView(Schedule schedule)
    {
        DoctorID = schedule.IdDoctor;
        Start =  schedule.Start;
        End = schedule.End;
    }
}