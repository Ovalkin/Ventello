using System.ComponentModel.DataAnnotations;

namespace Vintello.Common.DTOs;

public class UpdatedRoleDto
{
    [RegularExpression(@"^\D*$", ErrorMessage = "Имя может содержать только слова!")]
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
}