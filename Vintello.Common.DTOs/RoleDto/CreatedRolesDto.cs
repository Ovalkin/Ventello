using System.ComponentModel.DataAnnotations;

namespace Vintello.Common.DTOs;

public class CreatedRolesDto
{
    [Required(ErrorMessage = "Имя роли обязтельно!")]
    [RegularExpression(@"^\D*$", ErrorMessage = "Имя может содержать только слова!")]
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
}