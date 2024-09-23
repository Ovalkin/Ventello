using System.Text.Json.Serialization;

namespace Vintello.Common.DTOs;

public class CreatedCategoryDto
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
    public List<RetrivedItemsDto> Items { get; set; } = new();
}
public class UpdatedCategoryDto
{
    [JsonIgnore]
    public int? Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
