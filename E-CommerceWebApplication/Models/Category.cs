using E_CommerceWebApplication.Data;

namespace E_CommerceWebApplication.Models;

public class Category
{
    public int Id { get; set; }
   
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageURL { get; set; }
}
