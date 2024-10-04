using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public abstract class CategoryDto
{
    public static Category CreateCategory(CategoryDto categoryDto)
    {
        return categoryDto.GetCategory(categoryDto);
    }

    public static TCategoryDto CreateDto<TCategoryDto>(Category category) 
        where TCategoryDto : CategoryDto, new()
    {
        TCategoryDto thisDto = new TCategoryDto();
        return (TCategoryDto)thisDto.GetCategoryDto(category);
    }

    public static IEnumerable<TCategoryDto> CreateDto<TCategoryDto>(IEnumerable<Category> categories)
        where TCategoryDto : CategoryDto, new()
    {
        TCategoryDto thisDto = new TCategoryDto();
        return (IEnumerable<TCategoryDto>)thisDto.GetCategoriesDto(categories);
    }

    public static Category CreateCategory<TCategoryDto>(TCategoryDto categoryDto, Category category)
        where TCategoryDto : CategoryDto, new()
    {
        TCategoryDto thisDto = new TCategoryDto();
        return thisDto.UpdateCategory(categoryDto, category);
    }

    protected abstract Category GetCategory<TCategoryDto>(TCategoryDto categoryDto);
    protected abstract CategoryDto GetCategoryDto(Category category);
    protected abstract IEnumerable<CategoryDto> GetCategoriesDto(IEnumerable<Category> categories);
    protected abstract Category UpdateCategory<TCategoryDto>(TCategoryDto categoryDto, Category category);
}