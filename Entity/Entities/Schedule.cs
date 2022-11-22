namespace Entity;
public class Schedule {
    private int _idDoctor;
    private DateTime _start;
    private DateTime _end;
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
