using System.ComponentModel.DataAnnotations;

namespace Vintello.Common.DTOs;

public class CreatedCategoryDto
{
    [Required(ErrorMessage = "Имя обязательно!")]
    [RegularExpression(@"^\D*$", ErrorMessage = "Имя может содержать только слова!")]
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

public class UpdatedCategoryDto
{
    [RegularExpression(@"^\D*$", ErrorMessage = "Имя может содержать только слова!")]
    public string? Name { get; set; }
    public string? Description { get; set; }
}