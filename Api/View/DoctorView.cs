using System.Text.Json.Serialization;
using Entity;

namespace Api;

public class DoctorView
{
    [JsonPropertyName("id")]
    public int ID { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
    [JsonPropertyName("specialization")]
    public string Specialization { get; set; } = "";

    public DoctorView(Doctor doctor)
    {
        ID = doctor.ID;
        Name = doctor.Name;
        Specialization = doctor.Specialization;
    }
}