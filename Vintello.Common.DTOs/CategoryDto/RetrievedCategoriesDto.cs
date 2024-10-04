using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class RetrievedCategoriesDto : CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
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
        List<RetrievedCategoriesDto> result = new();
        foreach (Category category in categories)
        {
            result.Add(new RetrievedCategoriesDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            });
        }
        return result.AsEnumerable();
    }

    protected override Category UpdateCategory<TCategoryDto>(TCategoryDto categoryDto, Category category)
    {
        throw new NotImplementedException();
    }
}