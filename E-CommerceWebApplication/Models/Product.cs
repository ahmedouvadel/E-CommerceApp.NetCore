using E_CommerceWebApplication.Models;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; } 
    public string ImageURL { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int CategoryID { get; set; }
    public Category Category { get; set; } 
}

