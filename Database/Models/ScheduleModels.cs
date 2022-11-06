using System.ComponentModel.DataAnnotations;
namespace Database;

public class ScheduleModels
{
    [Required]
    public int IdDoctor { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public Schedule(int idDoctor, DateTime start, DateTime end)
    {
        IdDoctor = idDoctor;
        Start = start;
        End = end;
    }
}