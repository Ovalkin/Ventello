using System.ComponentModel.DataAnnotations;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class CreatedCategoryDto
{
    [Required(ErrorMessage = "Имя обязательно!")]
    [RegularExpression(@"^\D*$", ErrorMessage = "Имя может содержать только слова!")]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public static Category CreateCategory(CreatedCategoryDto categoryDto)
    {
        return new Category
        {
            Name = categoryDto.Name,
            Description = categoryDto.Description
        };
    }
}