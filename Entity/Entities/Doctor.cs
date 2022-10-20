public class Doctor {
    private int _id;
    private string _name;
    private Specialization _specialization;
    public int Id { get; set; }
    public string Name { get; set; }
    public Specialization Specialization { get; set; }
    public Doctor(int id, string name, Specialization specialization)
    {
        Id = id;
        Name = name;
        Specialization = specialization;
    }
}
