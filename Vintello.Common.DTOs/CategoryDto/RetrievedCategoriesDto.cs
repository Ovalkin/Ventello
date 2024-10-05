using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class RetrievedCategoriesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public static IEnumerable<RetrievedCategoriesDto> CreateDto(IEnumerable<Category> categories)
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
}