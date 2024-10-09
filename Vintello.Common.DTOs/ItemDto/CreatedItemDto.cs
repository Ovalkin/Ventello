using System.ComponentModel.DataAnnotations;
using Vintello.Common.EntityModel.PostgreSql;

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
    [RegularExpression(@"(^\d*,\d*$)|(^\d*$)")]
    [Range(0, 1_000_000_000, ErrorMessage = "Цена не должна находиться в диапазоне от 0 - 1 000 000 000!")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Фотографии обязательны!")]
    public List<string> Images { get; set; } = null!;
    
    public static Item CreateItem(CreatedItemDto itemDto)
    {
        return new Item
        {
            UserId = itemDto.UserId,
            CategoryId = itemDto.CategoryId,
            Title = itemDto.Title,
            Description = itemDto.Description,
            Price = itemDto.Price,
            Images = itemDto.Images,
            Status = "created",
            CreatedAt = DateTime.Now
        };
    }
}