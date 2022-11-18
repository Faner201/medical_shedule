namespace Entity;
public class Schedule {
    private int _idDoctor;
    private int _idUser;
    private DateTime _start;
    private DateTime _end;
    private Specialization _specialization;
    public int IdUser { get; set; }
    public int IdDoctor { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Specialization SpecializationDoctor { get; set; }

    public Schedule(int idDoctor, int idUser, DateTime start, DateTime end, Specialization specialization)
    {
        IdDoctor = idDoctor;
        IdUser = idUser;
        Start = start;
        End = end;
        SpecializationDoctor = specialization;
    }
}
