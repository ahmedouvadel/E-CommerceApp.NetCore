using E_CommerceWebApplication.Models;
using E_CommerceWebApplication.Repository;

public interface ICategoryRepository: IRepository<Category>
{
    Task<List<Product>> GetProducts(int id);
    Category Get(string name);
}
