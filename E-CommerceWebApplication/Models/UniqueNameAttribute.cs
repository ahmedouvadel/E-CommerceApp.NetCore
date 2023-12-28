using E_CommerceWebApplication.Data;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceWebApplication.Models;

public class UniqueNameAttribute:ValidationAttribute
{
    private readonly ICategoryRepository _categoryRepository;

    public UniqueNameAttribute(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        
        Category category = validationContext.ObjectInstance as Category;
        string name = value.ToString();
        Category categoryFromDB = _categoryRepository.Get(name);

        
        if(category.Id == 0)
        {
            if(categoryFromDB == null)
                return ValidationResult.Success;

            return new ValidationResult("This Name Is Already Exists");
            
        }
        if (categoryFromDB != null && categoryFromDB.Id == category.Id)
            return ValidationResult.Success;

        return new ValidationResult("This Name Is Already Exists");


    }
}
