using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class RetrievedCategoryDto : CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<RetrievedItemDto> Items { get; set; } = new();

    protected override CategoryDto GetCategoryDto(Category category)
    {
        return new RetrievedCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }

    protected override IEnumerable<CategoryDto> GetCategoriesDto(IEnumerable<Category> category)
    {
        throw new NotImplementedException();
    }

    protected override Category UpdateCategory<TCategoryDto>(TCategoryDto categoryDto, Category category)
    {
        throw new NotImplementedException();
    }

    protected override Category GetCategory<TCategoryDto>(TCategoryDto categoryDto)
    {
        throw new NotImplementedException();
    }
}
