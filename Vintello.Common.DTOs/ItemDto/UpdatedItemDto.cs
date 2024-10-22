using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class UpdatedItemDto
{
    [RegularExpression(@"^\d*$")]
    public int? CategoryId { get; set; }
    [RegularExpression(@"^\D*$")]
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; } 
    [RegularExpression(@"(^\d*,\d*$)|(^\d*$)")]
    public decimal? Price { get; set; }
    public List<string>? Images { get; set; }
    public string? UserId { get; set; }
    
    public static Item CreateItem(UpdatedItemDto updatedItemDto, Item item)
    {
        if (updatedItemDto.CategoryId != null)
            item.CategoryId = (int)updatedItemDto.CategoryId;
        if (updatedItemDto.UserId != null)
            item.UserId = int.Parse(updatedItemDto.UserId);
        if (updatedItemDto.Title != null)
            item.Title = updatedItemDto.Title;
        if (updatedItemDto.Description != null)
            item.Description = updatedItemDto.Description;
        if (updatedItemDto.Status != null)
            item.Status = updatedItemDto.Status;
        if (updatedItemDto.Price != null)
            item.Price = (decimal)updatedItemDto.Price;
        if (updatedItemDto.Images != null)
            item.Images = updatedItemDto.Images;
        item.UpdatedAt = DateTime.Now;
        return item;
    }
}