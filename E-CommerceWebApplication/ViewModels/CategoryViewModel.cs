using System.ComponentModel;
namespace E_CommerceWebApplication.ViewModels;
public class CategoryViewModel
{
    public string Name { get; set; }
    [DisplayName("Image ")]
    public IFormFile? Image { get; set; }
    public string Description { get; set; }
}
