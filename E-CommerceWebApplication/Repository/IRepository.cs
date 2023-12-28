namespace E_CommerceWebApplication.Repository;

public interface IRepository<T>
{
    Task<List<T>> GetAll();
    //Task<List<T>> Search(string key);
    void Create(T entity);
    void Update(T entity);
    void Delete(int id);
    Task<T> Get(int id);
}
