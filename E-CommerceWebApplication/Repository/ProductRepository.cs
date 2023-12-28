using E_CommerceWebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceWebApplication.Repository;
public class ProductRepository : Repository<Product>,IProductRepository 
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context):base(context)
    {
        _context = context;
    }
    
    public async Task<List<Product>> GetAll()
        => await _context.Products.Include(p => p.Category).OrderBy(p => p.CategoryID).ToListAsync();
    public async Task<List<Product>> SameCategory(int id)
        =>await _context.Products.Where(p=>p.CategoryID == id).ToListAsync();

    public async Task<List<Product>> Search(string key)
    {
        return await _context.Products
            .Where(p => EF.Functions.Like(p.Name, $"%{key}%") || EF.Functions.Like(p.Description, $"%{key}%"))
            .Include(p => p.Category)
            .ToListAsync();
    }

}
