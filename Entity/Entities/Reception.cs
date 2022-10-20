public class Reception {
    private DateTime _begin;
    private DateTime _end;
    private int _idUser;
    private int _idDoctor;
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public int IdUser { get; }
    public int IdDoctor { get; }
    public Reception(DateTime begin, DateTime end, int idUser, int idDoctor)
    {
        Begin = begin;
        End = end;
        IdUser = idUser;
        IdDoctor = idDoctor;
    }
}
