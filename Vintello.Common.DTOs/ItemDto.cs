namespace Vintello.Common.DTOs;

public class CreateItemDto
{
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public string[] Images { get; set; } = null!;
}