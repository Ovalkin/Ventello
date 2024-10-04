using System.ComponentModel.DataAnnotations;

namespace Vintello.Common.DTOs;

public class UpdatedItemDto
{
    [RegularExpression(@"^\d*$")]
    public int? CategoryId { get; set; }
    [RegularExpression(@"^\D*$")]
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; } 
    [RegularExpression(@"^\d*$")]
    public decimal? Price { get; set; }
    public List<string>? Images { get; set; }
}