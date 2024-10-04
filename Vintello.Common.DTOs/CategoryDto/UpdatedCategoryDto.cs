using System.ComponentModel.DataAnnotations;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class UpdatedCategoryDto : CategoryDto
{
    [RegularExpression(@"^\D*$", ErrorMessage = "Имя может содержать только слова!")]
    public string? Name { get; set; }

    public string? Description { get; set; }

    protected override Category GetCategory<TCategoryDto>(TCategoryDto categoryDto)
    {
        throw new NotImplementedException();
    }

    protected override CategoryDto GetCategoryDto(Category category)
    {
        throw new NotImplementedException();
    }

    protected override IEnumerable<CategoryDto> GetCategoriesDto(IEnumerable<Category> categories)
    {
        throw new NotImplementedException();
    }

    protected override Category UpdateCategory<TCategoryDto>(TCategoryDto categoryDto, Category category)
    {
        UpdatedCategoryDto updatedCategoryDto = (categoryDto as UpdatedCategoryDto)!;
        if (updatedCategoryDto.Name != null) 
            category.Name = updatedCategoryDto.Name;
        
        if (updatedCategoryDto.Description != null)
            category.Description = updatedCategoryDto.Description;
        
        return category;
    }
}