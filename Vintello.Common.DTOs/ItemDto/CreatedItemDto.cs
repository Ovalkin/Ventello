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