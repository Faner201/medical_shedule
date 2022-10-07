 public interface IRepository<T> : IDisposable
    where T: class
{
    IEnumerable<T> GetList();
    T Get(int id);
    void Create(T item);
    void Update(T item);
    void Delete(int id);
    void Save();
    bool Include(T item);
}