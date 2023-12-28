namespace E_CommerceWebApplication.Repository;

public interface IProductRepository: IRepository<Product>
{
    Task<List<Product>> Search(string key);
    Task<List<Product>> SameCategory(int id);



}
