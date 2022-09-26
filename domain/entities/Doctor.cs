class Doctor {
    private int _id;
    private string _name;
    private Specialization specialization;
    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public Specialization Specialization { get => specialization; set => specialization = value; }
}