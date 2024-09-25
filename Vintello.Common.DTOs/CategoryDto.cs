using System.Text.Json.Serialization;

namespace Vintello.Common.DTOs;

public class CreatedUpdatedCategoryDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class RetrivedCategoriesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
public class RetrivedCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<RetrivedItemDto> Items { get; set; } = new();
}