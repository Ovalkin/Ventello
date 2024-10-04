using System.ComponentModel.DataAnnotations;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class CreatedCategoryDto : CategoryDto
{
    [Required(ErrorMessage = "Имя обязательно!")]
    [RegularExpression(@"^\D*$", ErrorMessage = "Имя может содержать только слова!")]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    protected override Category GetCategory<TCategoryDto>(TCategoryDto categoryDto)
    {
        CreatedCategoryDto createdCategoryDto = (categoryDto as CreatedCategoryDto)!;
        return new Category { Name = createdCategoryDto.Name, Description = createdCategoryDto.Description };
    }

    protected override CategoryDto GetCategoryDto(Category category)
    {
        throw new NotImplementedException();
    }

    protected override IEnumerable<CategoryDto> GetCategoriesDto(IEnumerable<Category> category)
    {
        throw new NotImplementedException();
    }

    protected override Category UpdateCategory<TCategoryDto>(TCategoryDto categoryDto, Category category)
    {
        throw new NotImplementedException();
    }
}