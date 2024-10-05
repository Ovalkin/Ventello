using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class RetrievedItemDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public decimal? Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<string>? Images { get; set; }
    
    public static RetrievedItemDto CreateDto(Item item)
    {
        return new RetrievedItemDto
        {
            Id = item.Id,
            UserId = item.UserId,
            CategoryId = item.CategoryId,
            Title = item.Title,
            Description = item.Description,
            Status = item.Status,
            Price = item.Price,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt,
            Images = item.Images
        };
    }
    public static IEnumerable<RetrievedItemDto?> CreateDto(IEnumerable<Item> items)
    {
        List<RetrievedItemDto?> result = new();
        foreach (var item in items)
        {
            result.Add(CreateDto(item));
        }
        return result.AsEnumerable();
    }
}