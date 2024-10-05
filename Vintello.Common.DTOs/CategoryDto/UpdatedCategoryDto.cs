using System.ComponentModel.DataAnnotations;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class UpdatedCategoryDto
{
    [RegularExpression(@"^\D*$", ErrorMessage = "Имя может содержать только слова!")]
    public string? Name { get; set; }
    public string? Description { get; set; }

    public static Category CreateCategory(UpdatedCategoryDto categoryDto, Category category)
    {
        if (categoryDto.Name != null) 
            category.Name = categoryDto.Name;
        if (categoryDto.Description != null)
            category.Description = categoryDto.Description;
        
        return category;
    }
}