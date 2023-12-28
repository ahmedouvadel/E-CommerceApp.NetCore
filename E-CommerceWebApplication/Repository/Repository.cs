using E_CommerceWebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceWebApplication.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(T entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
    }

    public async void Delete(int id)
    {
        _context.Remove(await Get(id));
        _context.SaveChanges();
    }

    public async Task<T> Get(int id) => await _context.Set<T>().FindAsync(id);

    public async Task<List<T>> GetAll() => await _context.Set<T>().ToListAsync();

    public void Update(T entity)
    {
        _context.Update(entity);
        _context.SaveChanges();
    }
}
