public class Reception {
    private DateTime _begin;
    private DateTime _end;
    private int _idUser;
    private int _idDoctor;
    public DateTime Begin { get => _begin; set => _begin = value; }
    public DateTime End { get => _end; set => _end = value; }
    public int IdUser { get => _idUser; }
    public int IdDoctor { get => _idDoctor; }
}
