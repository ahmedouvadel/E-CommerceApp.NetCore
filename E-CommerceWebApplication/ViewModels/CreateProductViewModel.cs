using E_CommerceWebApplication.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceWebApplication.ViewModels;
public class CreateProductViewModel
{
    public int ID { get; set; }
    public string Name { get; set; }
    public IFormFile? Image { get; set; }
    public string Description { get; set; }
    [Range(1, double.MaxValue,ErrorMessage ="Only Positive Numbers Allowed ")]
    
    public double Price { get; set; }
    [DisplayName("Category Name")]
    public int CategoryId { get; set; }
    public List<Category>? Categories { get; set; }
}
