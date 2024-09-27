using System.ComponentModel.DataAnnotations;

namespace Vintello.Common.DTOs;

public class CreatedItemDto
{
    [Required(ErrorMessage = "ID Пользователя обязательно!")]
    [RegularExpression(@"^\d*$")]
    public int UserId { get; set; }
    [Required(ErrorMessage = "ID Категории обязательно!")]
    [RegularExpression(@"^\d*$")]
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "Название товара обязтельно!")]
    [RegularExpression(@"^\D*$")]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    [Required(ErrorMessage = "Цена обязательна!")]
    [RegularExpression(@"^\d*$")]
    [Range(0, 1_000_000_000, ErrorMessage = "Цена не должна находиться в диапазоне от 0 - 1 000 000 000!")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Фотографии обязательны!")]
    public string[] Images { get; set; } = null!;
}
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
}

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