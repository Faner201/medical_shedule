namespace Entity;
public class Specialization {
    private int _id;
    private string _name;

    public int Id { get; set; }
    public string Name { get; set; }

    public Specialization(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
