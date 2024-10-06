using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class RetrievedCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<RetrievedItemDto>? Items { get; set; } = new();

    public static RetrievedCategoryDto CreateDto(Category category)
    {
        return new RetrievedCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Items = RetrievedItemDto.CreateDto(category.Items).ToList()
        };
    }
}
