using System.Text.Json.Serialization;
using Entity;

namespace Api;

public class ReceptionView
{
    [JsonPropertyName("userID")]
    public int UserID { get; set; }
    [JsonPropertyName("doctorID")]
    public int DoctorID { get; set; }
    [JsonPropertyName("start")]
    public DateTime Start { get; set; }
    [JsonPropertyName("end")]
     public DateTime End { get; set; }
    [JsonPropertyName("specializationDoctor")]
    public Specialization SpecializationDoctor { get; set; }

    public ReceptionView(Reception reception)
    {
        DoctorID = reception.DoctorID;
        UserID = reception.UserID;
        Start = reception.Start;
        End = reception.End;
        SpecializationDoctor = reception.SpecializationDoctor;
    }
}