class Reception {
    private DateTime begin;
    private DateTime end;
    private int _idUser;
    private int _idDoctor;
    public DateTime Begin { get => begin; set => begin = value; }
    public DateTime End { get => end; set => end = value; }
    public int IdUser { get => _idUser; }
    public int IdDoctor { get => _idDoctor; }
}