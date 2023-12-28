using System.ComponentModel;
namespace E_CommerceWebApplication.ViewModels;
public class ProductDetails
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string ImageURL { get; set; }
    public string Description { get; set; }
    [DisplayName("Category Name")]
    public string CategoryName { get;set; }
}
